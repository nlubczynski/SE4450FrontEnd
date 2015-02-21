namespace FsWeb.Models
 
open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open System.Runtime.Serialization
open System.Data


[<Table("SensorReading")>]
type SensorReadingRaw() = 
    let mutable id = ""
    let mutable sensorId = ""
    let mutable time = ""
    let mutable value = ""
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.SensorId with get() = sensorId and set v = sensorId <- v
    [<Required>] member x.Time with get() = time and set v = time <- v
    [<Required>] member x.Value with get() = value and set v = value <- v

type SensorReading() = 
    let mutable id = ""
    let mutable sensorId = ""
    let mutable time = ""
    let mutable value = ""
    let mutable sensor = new FsWeb.Models.SensorModel()
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.SensorId with get() = sensorId and set v = sensorId <- v
    [<Required>] member x.Time with get() = time and set v = time <- v
    [<Required>] member x.Value with get() = value and set v = value <- v
    [<Required>] member x.Sensor with get() = sensor and set v = sensor <- v

type SensorReadingMinimal(value : string, time: string) =
    let mutable y = value
    let mutable x = (DateTime.Parse(time) - new DateTime(1970, 1, 1)).TotalSeconds

