namespace FsWeb.Models
 
open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema

[<Table("Sensors")>]
type SensorRaw() = 
    let mutable id = ""
    let mutable unit = ""
    let mutable building = ""
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.Unit with get() = unit and set v = unit <- v
    [<Required>] member x.Building with get() = building and set v = building <- v

type SensorModel() = 
    let mutable id = ""
    let mutable unit = ""
    let mutable building = new FsWeb.Models.Building()
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.Unit with get() = unit and set v = unit <- v
    [<Required>] member x.Building with get() = building and set v = building <- v