namespace FsWeb.Models
 
open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema

[<Table("Unit")>]
type Unit() = 
    let mutable id = ""
    let mutable name = ""
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.Name with get() = name and set v = name <- v
