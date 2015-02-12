namespace FsWeb.Utilities
 
open FsWeb.Models

module UtilityFunctions =

    let RawSensorToModel(raw : FsWeb.Models.SensorRaw, buildings : List<Building>, units : List<Unit>) =
        // Create the return model
        let mutable model = new FsWeb.Models.SensorModel()
        model.Id <- raw.Id
        
        // find and replace the unit
        let unitResult = List.find(fun (unit : Unit) -> unit.Id = raw.Unit) units
        model.Unit <- unitResult.Name

        // find and replace the name
        let buildingResult = List.find(fun (building : Building) -> building.Id = raw.Building) buildings
        model.Building <- buildingResult

        // return
        model

    let RawSensorReadingToModel(raw : FsWeb.Models.SensorReadingRaw, sensors : List<SensorModel> ) =
        // Copy values
        let mutable model = new FsWeb.Models.SensorReading()
        model.Id <- raw.Id
        model.SensorId <- raw.SensorId
        model.Time <- raw.Time
        model.Value <- raw.Value

        // Find and set the sensor model
        let sensor = List.find(fun (sen : SensorModel) -> sen.Id = raw.SensorId) sensors
        model.Sensor <- sensor

        //return 
        model