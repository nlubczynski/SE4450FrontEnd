namespace FsWeb.Utilities
 
open FsWeb.Models

module UtilityFunctions =

    let RawToModel(raw : FsWeb.Models.SensorRaw, buildings : List<Building>, units : List<Unit>) =
        // Create the return model
        let mutable model = new FsWeb.Models.SensorRaw()
        model.Id <- raw.Id
        
        // find and replace the unit
        let unitResult = List.find(fun (unit : Unit) -> unit.Id = raw.Unit) units
        model.Unit <- unitResult.Name

        // find and replace the name
        let buildingResult = List.find(fun (building : Building) -> building.Id = raw.Building) buildings
        model.Building <- buildingResult.Name

        // return
        model