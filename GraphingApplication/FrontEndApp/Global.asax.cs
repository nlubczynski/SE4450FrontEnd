using FrontEndApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FrontEndApp
{

    public class MvcApplication : System.Web.HttpApplication
    {
        // Main repository
        private Powersmiths data;

        // Data sets
        private List<Sensor> _sensors;
        public List<Sensor> Sensors
        {
            get
            {
                sensorMutex.WaitOne();
                List<Sensor> returnVal = _sensors.ConvertAll(sensor => new Sensor(sensor));
                sensorMutex.ReleaseMutex();
                return returnVal;
            }
            private set { _sensors = value; }
        }

        private List<SensorReading> _sensorReadings;
        public List<SensorReading> SensorReadings
        {
            get
            {
                sensorReadingMutex.WaitOne();
                List<SensorReading> returnVal = _sensorReadings.ConvertAll(sensorReading => new SensorReading(sensorReading));
                sensorReadingMutex.ReleaseMutex();
                return returnVal;
            }
            private set { _sensorReadings = value; }
        }

        private List<Building> _buidlings;
        public List<Building> Buildings
        {
            get
            {
                buildingMutex.WaitOne();
                List<Building> returnVal = _buidlings.ConvertAll(building => new Building(building));
                buildingMutex.ReleaseMutex();
                return returnVal;
            }
            private set { _buidlings = value; }
        }

        private List<Unit> _units;
        public List<Unit> Units
        {
            get
            {
                unitMutex.WaitOne();
                List<Unit> returnVal = _units.Select(unit => unit).ToList();
                unitMutex.ReleaseMutex();
                return returnVal;
            }
            private set { _units = value; }
        }

        // Instance
        public static MvcApplication Instance { get; private set; }

        // Mutext for retrieving data
        private static Mutex buildingMutex = new Mutex();
        private static Mutex sensorMutex = new Mutex();
        private static Mutex sensorReadingMutex = new Mutex();
        private static Mutex unitMutex = new Mutex();

        // Main application entry point
        protected void Application_Start()
        {
            if (Instance == null)
                Instance = this;

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initiliaze
            data = new Powersmiths();
            Sensors = new List<Sensor>();
            SensorReadings = new List<SensorReading>();
            update();

            // Update loop
            Thread updateThread = new Thread(updateLoop);
            updateThread.Start();
        }

        private void updateLoop()
        {
            for (; ; )
            {
                update();
            }
        }

        private void update()
        {
            List<Sensor> swapSensors                = new List<Sensor>();
            List<SensorReading> swapSensorReadings  = new List<SensorReading>();
            List<Building> swapBuildings            = new List<Building>();
            List<Unit> swapUnits                    = new List<Unit>();


            // Sensors
            swapSensors = data.Sensors.ToList();

            // Sensor Readings
            swapSensorReadings = data.SensorReadings.ToList();

            // Buildings
            swapBuildings = data.Buildings.ToList();

            // Units
            swapUnits = data.Units.ToList();

            // Swap Buildings
            buildingMutex.WaitOne();
            Buildings = swapBuildings;
            buildingMutex.ReleaseMutex();

            // Swap sensors
            sensorMutex.WaitOne();
            Sensors = swapSensors;
            sensorMutex.ReleaseMutex();

            // Swap sensorReadings
            sensorReadingMutex.WaitOne();
            SensorReadings = swapSensorReadings;
            sensorReadingMutex.ReleaseMutex();

            // Swap units
            unitMutex.WaitOne();
            Units = swapUnits;
            unitMutex.ReleaseMutex();
        }

    }
}