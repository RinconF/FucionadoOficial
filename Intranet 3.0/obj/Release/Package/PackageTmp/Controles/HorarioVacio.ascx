<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorarioVacio.ascx.cs" Inherits="Intranet_3._0.HorarioVacio" %>

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
            <asp:TableCell HorizontalAlign="Center">
                <asp:Label ID="lblProd" runat="server" Text="Producción"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center">
                <asp:Label ID="lblInfoAsig" runat="server" Text="Descanso"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Center">
                <asp:Label ID="lblInfoProd" runat="server" Text=""></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Center">
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" CssClass="Horas_Trabajo">
                <asp:Label ID="lblPatio" runat="server" Text=" - "></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>
