namespace FsWeb.Models
 
open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open System.Runtime.Serialization
open System.Data


[<Table("SensorReading")>]
type SensorReadingRaw() = 
    let mutable id = 0
    let mutable sensorId = 0
    let mutable time = new System.DateTime()
    let mutable value = 0.0
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.SensorId with get() = sensorId and set v = sensorId <- v
    [<Required>] member x.Time with get() = time and set v = time <- v
    [<Required>] member x.Value with get() = value and set v = value <- v

type SensorReading() = 
    let mutable id = 0
    let mutable sensorId = 0
    let mutable time = new System.DateTime()
    let mutable value = 0.0
    let mutable sensor = new FsWeb.Models.SensorModel()
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.SensorId with get() = sensorId and set v = sensorId <- v
    [<Required>] member x.Time with get() = time and set v = time <- v
    [<Required>] member x.Value with get() = value and set v = value <- v
    [<Required>] member x.Sensor with get() = sensor and set v = sensor <- v

type SensorReadingMinimal(value : float, time : System.DateTime) =
    let mutable y = value
    let mutable x = (time - new DateTime(1970, 1, 1)).TotalMilliseconds
    member this.X with get() = x
    member this.Y with get() = y

// for serialization from JSON to F# types representing a SensorReading
type JSONSensorReading = {
     id: int
     timestamp: System.DateTime
     value: int
     }