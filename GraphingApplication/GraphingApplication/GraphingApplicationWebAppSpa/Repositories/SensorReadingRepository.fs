namespace FsWeb.Repositories
 
open System.Data.Entity
open FsWeb.Models
 
type SensorReadingsContext() =
    inherit DbContext("Powersmiths")
 
    do Database.SetInitializer(new CreateDatabaseIfNotExists<SensorReadingsContext>())
 
    [<DefaultValue()>]
    val mutable sensorReadings : IDbSet<SensorReadingRaw>

    member x.SensorReadings with get() = x.sensorReadings and set v = x.sensorReadings <- v
 
type SensorReadingsRepository() =
    
    member x.GetAll() =

        //Get all the buildings
        let sensorRepository = new SensorsRepository()
        let sensors = sensorRepository.GetAll()

        // Get the sensors, and apply the buildings name to them
        use context = new SensorReadingsContext()
        context.sensorReadings 
            |> Seq.toList
            |> List.map(fun sensorReading -> FsWeb.Utilities.UtilityFunctions.RawSensorReadingToModel(sensorReading, sensors))