#pragma checksum "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "368e5be7c3e6ff7648a202a0e16a61acce2c3ad2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Calculator_Calculate), @"mvc.1.0.view", @"/Views/Calculator/Calculate.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\_ViewImports.cshtml"
using Doctorantura.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\_ViewImports.cshtml"
using Doctorantura.App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
using Doctorantura.App.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"368e5be7c3e6ff7648a202a0e16a61acce2c3ad2", @"/Views/Calculator/Calculate.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e53eda28aaa331127c9d764d63aca181bfd2d0a", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Calculator_Calculate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CalculateVM>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
  
    ViewData["Title"] = "Calculate";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    .setFull {
        width: 100%;
    }
</style>


<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.css"" />



<div class=""row"">
    <div class=""container-fluid row"">
        <input id=""btn_add"" value=""Sütun əlavə et"" type=""button"" class="" btn btn-primary d-block col-12 ml-2"" />
    </div>

    <div class=""col-7"">
        <table class=""table"">
            <thead id=""thead"">
                <tr class=""tr_head"">
                    <th scope=""col"">G</th>
");
#nullable restore
#line 27 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                     foreach (var columnNum in @Model.ColumnLines.GroupBy(x => x.ColumnNum).Select(x => x.Key).ToList())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <th");
            BeginWriteAttribute("th_row", " th_row=\"", 812, "\"", 913, 1);
#nullable restore
#line 29 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 821, Model.ColumnLines.Where(x=>x.ColumnNum==columnNum).Select(x=>x.Column.Row).FirstOrDefault(), 821, 92, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" scope=\"col\">\r\n                            <input type=\"text\"");
            BeginWriteAttribute("value", " value=\"", 975, "\"", 1076, 1);
#nullable restore
#line 30 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 983, Model.ColumnLines.Where(x=>x.ColumnNum==columnNum).Select(x=>x.Column.Name).FirstOrDefault(), 983, 93, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull font-weight-bold\">\r\n                        </th>\r\n");
#nullable restore
#line 32 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tbody\">\r\n                <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 1282, "\"", 1303, 1);
#nullable restore
#line 37 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 1290, Model.TaskId, 1290, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"taskId\" />\r\n\r\n");
#nullable restore
#line 39 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                 foreach (var lineNum in @Model.ColumnLines.GroupBy(x => x.LineNum).Select(x => x.Key).ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"tr_tbody\">\r\n                        <td scope=\"col\">\r\n                            <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=\"", 1596, "\"", 1695, 1);
#nullable restore
#line 43 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 1604, Model.ColumnLines.Where(x=>x.ColumnNum==lineNum).Select(x=>x.Column.Name).FirstOrDefault(), 1604, 91, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull font-weight-bold\">\r\n                        </td>\r\n");
#nullable restore
#line 45 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                         foreach (var columnLine in @Model.ColumnLines.Where(x => x.LineNum == lineNum).OrderBy(x => x.Column.Row))
                        {
                            string disabled = "";

                            if (columnLine.Column.Row <= lineNum)
                                disabled = "disabled";


#line default
#line hidden
#nullable disable
            WriteLiteral("                            <td");
            BeginWriteAttribute("column", " column=\"", 2132, "\"", 2163, 1);
#nullable restore
#line 52 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 2141, columnLine.Column.Row, 2141, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("line", " line=\"", 2164, "\"", 2191, 1);
#nullable restore
#line 52 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 2171, columnLine.Line.Row, 2171, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                <input type=\"text\" onkeyup=\"lineNumChanged(this)\" ");
#nullable restore
#line 53 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                                                                             Write(disabled);

#line default
#line hidden
#nullable disable
            WriteLiteral(" value=");
#nullable restore
#line 53 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                                                                                             Write(decimal.Round(Convert.ToDecimal(columnLine.Value), 4));

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"setFull\">\r\n                            </td>\r\n");
#nullable restore
#line 55 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tr>\r\n");
#nullable restore
#line 57 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </tbody>\r\n            <tbody>\r\n                <tr id=\"tr_delete\">\r\n                    <td scope=\"col\"></td>\r\n");
#nullable restore
#line 63 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                     foreach (var columnNum in @Model.ColumnLines.GroupBy(x => x.ColumnNum).Select(x => x.Key).ToList())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td");
            BeginWriteAttribute("td_delete_row", " td_delete_row=\"", 2771, "\"", 2797, 1);
#nullable restore
#line 65 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 2787, columnNum, 2787, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("onclick", " onclick=\"", 2798, "\"", 2830, 3);
            WriteAttributeValue("", 2808, "deleteData(", 2808, 11, true);
#nullable restore
#line 65 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 2819, columnNum, 2819, 10, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2829, ")", 2829, 1, true);
            EndWriteAttribute();
            WriteLiteral(" scope=\"col\">\r\n                            <input type=\"button\" value=\"Sil\" ");
            WriteLiteral(" class=\"setFull btn btn-danger\">\r\n                        </td>\r\n");
#nullable restore
#line 68 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tr>\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"col-1\">\r\n        <table class=\"table\">\r\n            <thead id=\"thead_line_sum\">\r\n                <tr");
            BeginWriteAttribute("class", " class=\"", 3230, "\"", 3238, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <th>\r\n                        <input type=\"text\" disabled value=\"D\" class=\"setFull font-weight-bold\">\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tbody_sum\">\r\n");
#nullable restore
#line 84 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                 foreach (var lineNum in @Model.ColumnLines.GroupBy(x => x.LineNum).Select(x => x.Key).ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"tr_tbody_sum\">\r\n                        <td");
            BeginWriteAttribute("sumLine", " sumLine=", 3682, "", 3699, 1);
#nullable restore
#line 87 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 3691, lineNum, 3691, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=", 3757, "", 3883, 1);
#nullable restore
#line 88 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 3764, decimal.Round(Convert.ToDecimal(Model.LineSums.Where(x=>x.LineNum==lineNum).Select(x=>x.TotalSum).FirstOrDefault()),4), 3764, 119, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull\">\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 91 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=\"", 4078, "\"", 4156, 1);
#nullable restore
#line 94 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 4086, decimal.Round(Convert.ToDecimal(Model.LineSums.Sum(x=>x.TotalSum)),4), 4086, 70, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull\" />\r\n                    </td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n    <div class=\"col-1\">\r\n        <table class=\"table\">\r\n            <thead id=\"thead_line_w\">\r\n                <tr");
            BeginWriteAttribute("class", " class=\"", 4396, "\"", 4404, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <th>\r\n                        <input type=\"text\" disabled value=\"W\" class=\"setFull font-weight-bold\">\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tbody_w\">\r\n");
#nullable restore
#line 112 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                 foreach (var lineNum in @Model.ColumnLines.GroupBy(x => x.LineNum).Select(x => x.Key).ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"tr_tbody_w\">\r\n                        <td");
            BeginWriteAttribute("wLine", " wLine=\"", 4844, "\"", 4860, 1);
#nullable restore
#line 115 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 4852, lineNum, 4852, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=", 4919, "", 5040, 1);
#nullable restore
#line 116 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 4926, decimal.Round(Convert.ToDecimal(Model.WLines.Where(x=>x.LineNum==lineNum).Select(x=>x.Value).FirstOrDefault()),4), 4926, 114, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull\">\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 119 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n    <div class=\"col-1\">\r\n        <table class=\"table\">\r\n            <thead id=\"thead_line_alf\">\r\n                <tr");
            BeginWriteAttribute("class", " class=\"", 5308, "\"", 5316, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <th>\r\n                        <input type=\"text\" disabled value=\"α\" class=\"setFull font-weight-bold\">\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tbody_alf\">\r\n");
#nullable restore
#line 136 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                 foreach (var lineNum in @Model.ColumnLines.GroupBy(x => x.LineNum).Select(x => x.Key).ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"tr_tbody_alf\">\r\n                        <td");
            BeginWriteAttribute("alfLine", " alfLine=\"", 5760, "\"", 5778, 1);
#nullable restore
#line 139 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 5770, lineNum, 5770, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=", 5837, "", 5960, 1);
#nullable restore
#line 140 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 5844, decimal.Round(Convert.ToDecimal(Model.AlfLines.Where(x=>x.LineNum==lineNum).Select(x=>x.Value).FirstOrDefault()),4), 5844, 116, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull\">\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 143 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"col-1\">\r\n        <table class=\"table\">\r\n            <thead id=\"thead_line_qam\">\r\n                <tr");
            BeginWriteAttribute("class", " class=\"", 6226, "\"", 6234, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <th>\r\n                        <input type=\"text\" disabled value=\"γ\" class=\"setFull font-weight-bold\">\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tbody_qam\">\r\n");
#nullable restore
#line 159 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                 foreach (var lineNum in @Model.ColumnLines.GroupBy(x => x.LineNum).Select(x => x.Key).ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"tr_tbody_qam\">\r\n                        <td");
            BeginWriteAttribute("qamLine", " qamLine=\"", 6678, "\"", 6696, 1);
#nullable restore
#line 162 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 6688, lineNum, 6688, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=", 6755, "", 6878, 1);
#nullable restore
#line 163 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 6762, decimal.Round(Convert.ToDecimal(Model.QamLines.Where(x=>x.LineNum==lineNum).Select(x=>x.Value).FirstOrDefault()),4), 6762, 116, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull\">\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 166 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"col-1\">\r\n        <table class=\"table\">\r\n            <thead id=\"thead_line_x\">\r\n                <tr");
            BeginWriteAttribute("class", " class=\"", 7142, "\"", 7150, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <th>\r\n                        <input type=\"text\" disabled value=\"X\" class=\"setFull font-weight-bold\">\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tbody_x\">\r\n");
#nullable restore
#line 182 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                 foreach (var lineNum in @Model.ColumnLines.GroupBy(x => x.LineNum).Select(x => x.Key).ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"tr_tbody_x\">\r\n                        <td");
            BeginWriteAttribute("xLine", " xLine=\"", 7590, "\"", 7606, 1);
#nullable restore
#line 185 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 7598, lineNum, 7598, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=", 7665, "", 7786, 1);
#nullable restore
#line 186 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 7672, decimal.Round(Convert.ToDecimal(Model.XLines.Where(x=>x.LineNum==lineNum).Select(x=>x.Value).FirstOrDefault()),4), 7672, 114, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"setFull\">\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 189 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        <input type=\"text\" disabled");
            BeginWriteAttribute("value", " value=", 7981, "", 8053, 1);
#nullable restore
#line 192 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
WriteAttributeValue("", 7988, decimal.Round(Convert.ToDecimal(Model.XLines.Sum(x=>x.Value)),4), 7988, 65, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""setFull"">
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</div>
<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js""></script>


<script>
    let btnAdd = document.getElementById(""btn_add"");
    let t_head = document.getElementById(""thead"");
    let t_body = document.getElementById(""tbody"");
    let tr_tbody = document.getElementsByClassName(""tr_tbody"");
    let tr_head = document.getElementsByClassName(""tr_head"");
    let tr_delete = document.getElementById(""tr_delete"");
    let taskId = document.getElementById(""taskId"").value;


    let tbody_sum = document.getElementById(""tbody_sum"");
    let tr_tbody_sum = document.getElementsByClassName(""tr_tbody_sum"");

    let tbody_w = document.getElementById(""tbody_w"");
    let tr_tbody_w = document.getElementsByClassName(""tr_tbody_w"");

    let tbody_alf = document.getElementById(""tbody_alf"");
    let tr_tbody_alf = documen");
            WriteLiteral("t.getElementsByClassName(\"tr_tbody_alf\");\r\n\r\n    let tbody_qam = document.getElementById(\"tbody_qam\");\r\n    let tr_tbody_qam = document.getElementsByClassName(\"tr_tbody_qam\");\r\n\r\n    let maxColumn = ");
#nullable restore
#line 225 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
               Write(Model.ColumnLines.GroupBy(x => x.ColumnNum).Select(x => x.Key).LastOrDefault());

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n    let maxLine = ");
#nullable restore
#line 226 "D:\Metin\Projects\DoctoranturaApp\Doctorantura.App\Views\Calculator\Calculate.cshtml"
             Write(Model.ColumnLines.GroupBy(x => x.LineId).Select(x => x.Key).LastOrDefault());

#line default
#line hidden
#nullable disable
            WriteLiteral(@";

    let tbody_x = document.getElementById(""tbody_x"");
    let tr_tbody_x = document.getElementsByClassName(""tr_tbody_x"");

    //setTimeout(lineNumChanged(), 2000)
    setTimeout(addLineName(), 2000);


    btnAdd.addEventListener(""click"", function () {
        createTable();
        addLineName();
");
            WriteLiteral(@"            insertData(maxColumn)
        t_body.children[maxColumn - 1].firstElementChild.firstChild.value = t_head.firstElementChild.children[maxColumn].firstElementChild.value
    });

    function lineNumChanged(arg) {
        let columnNum = arg.parentElement.getAttribute(""column"");
        let lineNum = arg.parentElement.getAttribute(""line"");
        let value = arg.value;

");
            WriteLiteral(@"                            if (value != null && value != '' && value.length > 0) {
            UpdateData(columnNum, lineNum, value);
        }
    }

    ///////-=---------------------------==================----------------------==============-


    function createTable() {

        maxColumn++;
        maxLine++;
        createTh(`K${maxColumn}`);
        createTBodyTr();
        createTrSum();
        createTrW();
        createTrAlf();
        createTrQam();
        createTrX();
        createDeleteRow();
        for (let f of t_body.lastElementChild.children) {
            if (maxColumn > 1 && maxLine > 1)
                f.firstElementChild.setAttribute(""disabled"", """");
        }

        t_body.lastElementChild.lastElementChild.firstElementChild.value = 1;

        for (let f of t_body.children) {
            f.firstElementChild.firstElementChild.setAttribute(""disabled"", """")
        }
    }


    function addLineName() {
        for (let f = 0; f < t_head.firstEleme");
            WriteLiteral(@"ntChild.children.length; f++) {
            t_head.firstElementChild.children[f].addEventListener(""keyup"", function () {
                let colName = t_head.firstElementChild.children[f].firstElementChild.value;
                let colNum = t_head.firstElementChild.children[f].getAttribute(""th_row"");
                t_body.children[f - 1].firstElementChild.firstElementChild.value = colName;

                updateColumnName(colNum, colName);
            });
        }
    }


    //---------Qamma line -------------------


    function createTrQam() {
        let inputQam = document.createElement(""input"");
        inputQam.classList.add(""setFull"");
        inputQam.setAttribute(""disabled"", """")
        inputQam.value = 1;

        let tdQam = document.createElement(""td"");
        tdQam.setAttribute(""qamLine"", maxLine)
        tdQam.appendChild(inputQam);

        let trQam = document.createElement(""tr"");
        trQam.appendChild(tdQam);
        trQam.classList.add(""tr_tbody_qam"");
");
            WriteLiteral(@"
        tbody_qam.appendChild(trQam);
    }


    //---------Alfa line -------------------


    function createTrAlf() {
        let inputAlf = document.createElement(""input"");
        inputAlf.classList.add(""setFull"");
        inputAlf.setAttribute(""disabled"", """")
        inputAlf.value = 1;

        let tdAlf = document.createElement(""td"");
        tdAlf.setAttribute(""AlfLine"", maxLine)
        tdAlf.appendChild(inputAlf);

        let trAlf = document.createElement(""tr"");
        trAlf.appendChild(tdAlf);
        trAlf.classList.add(""tr_tbody_alf"");

        tbody_alf.appendChild(trAlf);
    }


    //---------W line -------------------


    function createTrW() {
        let inputW = document.createElement(""input"");
        inputW.classList.add(""setFull"");
        inputW.setAttribute(""disabled"", """")
        inputW.value = 1;

        let tdW = document.createElement(""td"");
        tdW.setAttribute(""WLine"", maxLine)
        tdW.appendChild(inputW);

        let trW");
            WriteLiteral(@" = document.createElement(""tr"");
        trW.appendChild(tdW);
        trW.classList.add(""tr_tbody_w"");

        tbody_w.appendChild(trW);
    }

    //---------line Sum -------------------

    function createTrSum() {
        let inputSum = document.createElement(""input"");
        inputSum.classList.add(""setFull"");
        inputSum.setAttribute(""disabled"", """")
        inputSum.value = 1;

        let tdSum = document.createElement(""td"");
        tdSum.setAttribute(""sumLine"", maxLine)
        tdSum.appendChild(inputSum);

        let trSum = document.createElement(""tr"");
        trSum.appendChild(tdSum);
        trSum.classList.add(""tr_tbody_sum"");

        tbody_sum.appendChild(trSum);
    }


    //---------X line -------------------


    function createTrX() {
        let inputX = document.createElement(""input"");
        inputX.classList.add(""setFull"");
        inputX.setAttribute(""disabled"", """")
        inputX.value = 1;

        let tdX = document.createElement(""td"")");
            WriteLiteral(@";
        tdX.setAttribute(""xLine"", maxLine)
        tdX.appendChild(inputX);

        let trX = document.createElement(""tr"");
        trX.appendChild(tdX);
        trX.classList.add(""tr_tbody_x"");

        tbody_x.appendChild(trX);
    }

    //base line--------------------

    function createTBodyTr() {
        let tBody_tr = document.createElement(""tr"");
        tBody_tr.classList.add(""tr_tbody"");

        for (let i = 0; i < tr_tbody.length + 2; i++) {
            let td = createTd(i, maxLine);
            tBody_tr.appendChild(td);
        }

        t_body.appendChild(tBody_tr);

    }

    function createTd(column, line) {
        let td_input = document.createElement(""input"");
        td_input.value = 0;
        td_input.classList.add(""setFull"")

        let td = document.createElement(""td"");

        if (column > 0 && line > 0) {
            td.setAttribute(""column"", column);
            td.setAttribute(""line"", line);
        }

        td.appendChild(td_input);");
            WriteLiteral(@"
        return td;
    }

    function createTh(name) {
        let th_input = document.createElement(""input"");
        th_input.value = name;
        th_input.classList.add(""setFull"")

        let th = document.createElement(""th"");
        th.setAttribute(""th_row"", maxColumn);

        th.appendChild(th_input);
        t_head.firstElementChild.appendChild(th);

        for (let i = 0; i < tr_head.length; i++) {

            for (let f = 0; f < t_body.children.length; f++) {
                let line = f + 1;
                let td = createTd(maxColumn, line);
                t_body.children[f].appendChild(td)
            }
        }
    }


    function createDeleteRow() {
        let del_input = document.createElement(""input"");
        del_input.value = ""Sil"";
        del_input.classList.add(""setFull"")
        del_input.classList.add(""btn"")
        del_input.classList.add(""btn-danger"")

        let td_del = document.createElement(""td"");
        td_del.setAttribute(""td_delete");
            WriteLiteral(@"_row"", maxColumn);

        td_del.appendChild(del_input);
        tr_delete.appendChild(td_del);
    }

    function UpdateData(column, line, val) {
        $.ajax({
            url: ""/Calculator/UpdateData"",
            method: ""POST"",
            data: { columnNo: column, lineNo: line, val: val, taskId: taskId },
            success: function (res) {
                location.reload();
            },
            error: function (res) {
                console.log(res.response);
            }
        });
    }

    function deleteData(columnNo) {

        console.log(columnNo);
        $.ajax({
            url: ""/Calculator/DeleteData"",
            method: ""POST"",
            data: { columnNo: columnNo, taskId: taskId },
            success: function (res) {
                location.reload();
            },
            error: function (res) {
                console.log(res.response);
            }
        });
    }

    function updateColumnName(columnNum, columnName) {
");
            WriteLiteral(@"        $.ajax({
            url: ""/Calculator/UpdateColumnName"",
            method: ""POST"",
            data: { columnNum: columnNum, columnName: columnName, taskId: taskId },
            success: function (res) {
                console.log(res.response);
            },
            error: function (res) {
                console.log(res.response);
            }
        });
    }

    function insertData(columnNum) {
        $.ajax({
            url: ""/Calculator/InsertData"",
            method: ""POST"",
            data: { columnNum: columnNum, taskId: taskId },
            success: function (res) {
                location.reload();
            },
            error: function (res) {
                console.log(res.response);
            }
        });
    }
</script>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CalculateVM> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
