Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports CrudApplication

Namespace Controllers
    Public Class ItemsController
        Inherits System.Web.Mvc.Controller

        Private db As New ItemContext

        ' GET: Items
        Function Index() As ActionResult
            Return View(db.Items.ToList())
        End Function

        ' GET: Items/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim item As Item = db.Items.Find(id)
            If IsNothing(item) Then
                Return HttpNotFound()
            End If
            Return View(item)
        End Function

        ' GET: Items/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Items/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Name,weight")> ByVal item As Item) As ActionResult
            If ModelState.IsValid Then
                db.Items.Add(item)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(item)
        End Function

        ' GET: Items/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim item As Item = db.Items.Find(id)
            If IsNothing(item) Then
                Return HttpNotFound()
            End If
            Return View(item)
        End Function

        ' POST: Items/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Name,weight")> ByVal item As Item) As ActionResult
            If ModelState.IsValid Then
                db.Entry(item).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(item)
        End Function

        ' GET: Items/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim item As Item = db.Items.Find(id)
            If IsNothing(item) Then
                Return HttpNotFound()
            End If
            Return View(item)
        End Function

        ' POST: Items/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim item As Item = db.Items.Find(id)
            db.Items.Remove(item)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
