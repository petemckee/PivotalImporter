<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PivotalImporter2.Web.Controllers.Models.LogOnModel>" %>
 <% using (Html.BeginForm("LogOn", "Account")) { %>
    <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
    <div>
        <fieldset>
            <div class="editor-label">
                <%: Html.LabelFor(m => m.UserName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.UserName) %>
                <%: Html.ValidationMessageFor(m => m.UserName) %>
            </div>
                
            <div class="editor-label">
                <%: Html.LabelFor(m => m.Password) %>
            </div>
            <div class="editor-field">
                <%: Html.PasswordFor(m => m.Password) %>
                <%: Html.ValidationMessageFor(m => m.Password) %>
            </div>
            <p>
                <button>Log On</button>
            </p>
        </fieldset>
    </div>
<% } %>