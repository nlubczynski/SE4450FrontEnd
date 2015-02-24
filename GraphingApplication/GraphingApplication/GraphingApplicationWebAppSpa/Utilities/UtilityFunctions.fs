namespace FsWeb.Utilities
 
open FsWeb.Models
open Newtonsoft.Json

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
        let sensor = List.find(fun (sen : SensorModel) -> System.Int32.Parse(sen.Id) = raw.SensorId) sensors
        model.Sensor <- sensor

        //return 
        model

    // serialize a JSON array of SensorReadings into a List of JSONSensorReading
    let SerializeJSONReading(json: string): List<JSONSensorReading> = 
        let json = JsonConvert.SerializeObject(json)
        JsonConvert.DeserializeObject<JSONSensorReading list>(json)

    // transforms a JSONSensorReading to an array representing the coordinates on a Highcharts graph
    let JSONSensorReadingToArray(reading: JSONSensorReading): float[] = 
        let r_min = SensorReadingMinimal(float(reading.value), reading.timestamp)
        [|r_min.X; r_min.Y|]

    // serializes a JSONSensor reading to a map id -> highcharts_coordinates
    let Serialize2DArray(readings: List<JSONSensorReading>): seq<int * float[][]> = 
        Seq.groupBy( fun r -> r.id) readings
        |> Seq.map( fun (id, rdings) -> 
            ( id, Seq.toArray(Seq.map( fun r -> JSONSensorReadingToArray(r) ) rdings) )
        )

    // gets the highcharts coordinates for a single sensor based on id (use this after Serialize2DArray)
    let getById(id: int, readings: seq<int * float[][]>): float[][] = 
        let (_, readings_for_id) = Seq.find( fun  (i, rdings) -> i.Equals id) readings
        readings_for_id
        
        
        

    