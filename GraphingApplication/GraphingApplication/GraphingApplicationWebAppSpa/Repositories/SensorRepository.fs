namespace FsWeb.Repositories
 
open System.Data.Entity
open FsWeb.Models
 
type SensorsContext() =
    inherit DbContext("Powersmiths")
 
    do Database.SetInitializer(new CreateDatabaseIfNotExists<SensorsContext>())
 
    [<DefaultValue()>]
    val mutable sensors : IDbSet<SensorRaw>

    member x.Sensors with get() = x.sensors and set v = x.sensors <- v
 
type SensorsRepository() =
    
    member x.GetAll() =

        //Get all the buildings
        let buildingsRepository = new BuildingsRepository()
        let buildings = buildingsRepository.GetAll()

        //Get all the units
        let unitRepository = new UnitRepository()
        let units = unitRepository.GetAll()

        // Get the sensors, and apply the buildings name to them
        use context = new SensorsContext()
        context.sensors 
            |> Seq.toList
            |> List.map(fun sensor -> FsWeb.Utilities.UtilityFunctions.RawToModel(sensor, buildings, units))
 
    member x.AddNew(building) =
 
        use context = new SensorsContext()
        context.sensors.Add(building) |> ignore
        context.SaveChanges()