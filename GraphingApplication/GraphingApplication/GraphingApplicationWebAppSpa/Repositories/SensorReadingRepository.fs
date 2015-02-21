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

        //Get all the sensors|
        let sensorRepository = new SensorsRepository()
        let sensors = sensorRepository.GetAll()

        // Get the sensors, and apply the buildings name to them
        use context = new SensorReadingsContext()
        context.sensorReadings 
            |> Seq.toList
            |> List.map(fun sensorReading -> FsWeb.Utilities.UtilityFunctions.RawSensorReadingToModel(sensorReading, sensors))

    member x.GetOne(id : string) =
        // Get all the sensors
        use context = new SensorReadingsContext()
        
        // Filter for right ID, create minimal value, create and return array
        context.sensorReadings 
            |> Seq.toList
            |> List.filter(fun sensorReadingRaw -> sensorReadingRaw.SensorId = System.Int32.Parse(id))
            |> List.map (fun sensorReadingRaw -> new SensorReadingMinimal(sensorReadingRaw.Value, sensorReadingRaw.Time))
            |> List.map (fun (sensorReadingMin : SensorReadingMinimal) -> [|sensorReadingMin.X; sensorReadingMin.Y|])
            |> List.toArray

    member x.GetOneAfterTime(id : string, timestamp : string) =
        // Get sensors after the timestamp
        use context = new SensorReadingsContext()

        let timeStampMySQL = new System.DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc)
        let timeStampMySQL = timeStampMySQL.AddMilliseconds(System.Double.Parse(timestamp))

        let result = query {
            for s in context.SensorReadings do
            where (s.Time >= timeStampMySQL)
            select s
        }

        // Filter for right ID, create minimal value, create and return array
        result
            |> Seq.toList
            |> List.filter(fun sensorReadingRaw -> sensorReadingRaw.SensorId = System.Int32.Parse(id))
            |> List.map (fun sensorReadingRaw -> new SensorReadingMinimal(sensorReadingRaw.Value, sensorReadingRaw.Time))
            |> List.map (fun (sensorReadingMin : SensorReadingMinimal) -> [|sensorReadingMin.X; sensorReadingMin.Y|])
            |> List.toArray
