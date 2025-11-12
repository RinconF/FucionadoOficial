<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorarioDia.ascx.cs" Inherits="Intranet_3._0.HorarioDia" %>

<div class="col-12 col-lg-6">
    <asp:Table runat="server" CssClass="Tabla_Horario">
        <asp:TableRow>
            <asp:TableCell RowSpan="2">
                <asp:Label ID="lblFecha" runat="server" Text="InfoFecha"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblAsignacion" runat="server" Text="Asignacion"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblAmplitud" runat="server" Text="Amplitud"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblProd" runat="server" Text="Producción"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblInfoAsig" runat="server" Text="InfoAsig"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblInfoAmpli" runat="server" Text="InfoAmp"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblInfoProd" runat="server" Text="InfoProd"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" CssClass="Horas_Trabajo">
                <asp:Label ID="lblpar" runat="server" Text="Parte de Trabajo 1 (04:20:00 - 08:24:00)"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4">
                <asp:GridView ID="gvPartOne" runat="server" GridLines="Vertical"></asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" CssClass="Horas_Trabajo">
                <asp:Label ID="lblpar2" runat="server" Text="Parte de Trabajo 2 (04:20:00 - 08:24:00)"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4">
                <asp:GridView ID="gvPartTwo" runat="server" GridLines="Vertical"></asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>
