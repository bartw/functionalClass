module CleverClass.Db

open System
open FSharp.Data.Sql

let [<Literal>] connStr = @"Data Source=DESKTOP-BPVORI2\SQLEXPRESS;Initial Catalog=CleverClass;User Id=fsharp;Password=fsharp"
let [<Literal>] dbVendor = Common.DatabaseProviderTypes.MSSQLSERVER
let [<Literal>] useOptTypes  = true

type Sql = 
    SqlDataProvider< dbVendor, 
                     connStr,   
                     UseOptionTypes = useOptTypes >

type DbContext = Sql.dataContext
type ClassGroup = DbContext.``dbo.classgroupsEntity``

let getContext () = Sql.GetDataContext()

let getClassGroups () : ClassGroup list =
    let ctx = getContext () 
    ctx.Dbo.Classgroups |> Seq.toList
    
let createClassGroup name =
    let ctx = getContext ()
    let createdClassGroup = ctx.Dbo.Classgroups.Create()
    createdClassGroup.Name <- name
    ctx.SubmitUpdates()
    createdClassGroup

let updateClassGroup (classGroup : ClassGroup) (name) =
    let ctx = getContext ()
    classGroup.Name <- name
    ctx.SubmitUpdates()