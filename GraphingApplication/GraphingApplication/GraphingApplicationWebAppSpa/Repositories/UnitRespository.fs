namespace FsWeb.Repositories
 
open System.Data.Entity
open FsWeb.Models
 
type UnitsContext() =
    inherit DbContext("Powersmiths")
 
    do Database.SetInitializer(new CreateDatabaseIfNotExists<UnitsContext>())
 
    [<DefaultValue()>]
    val mutable units : IDbSet<Unit>
 
    member x.Units with get() = x.units and set v = x.units <- v
 
type UnitRepository() =
    
    member x.GetAll() =
 
        use context = new UnitsContext()
        context.units |> Seq.toList
 
    member x.AddNew(building) =
 
        use context = new UnitsContext()
        context.units.Add(building) |> ignore
        context.SaveChanges()