﻿<%@ Page Language="C#"
    Inherits="Ict.Petra.WebServer.MConference.TImportPrintedBadgesUI"
    src="ImportPrintedBadges.aspx.cs" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Import Printed Badges</title>
<script type="text/javascript">
        function HideDialog() {
            parent.winImportPrintedBadges.hide();
            PrintedBadgesForm.getForm().reset();
        };
</script>        
    
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" CleanResourceUrl="false"/>
		<ext:FormPanel 
            ID="PrintedBadgesForm" 
            runat="server"
            Region="Center"
            Width="300"
            Frame="false"
            AutoHeight="true"
            MonitorValid="true"
            PaddingSummary="10px 10px 0 10px"
            LabelWidth="50">                
            <Defaults>
                <ext:Parameter Name="anchor" Value="95%" Mode="Value" />
                <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
            </Defaults>
            <Items>
                <ext:TextArea ID="RegistrationsKeys" DataIndex="RegistrationsKeys" runat="server" FieldLabel="Paste here from Excel" Height="360" Width="500"/>
                <ext:Label runat="server" Text="Please paste from Excel. Column1 is Date Printed, Column2 is Firstname, Column3 is LastName, Column4 is PartnerKey, Column5 is fellowshipgroupcode. "/>
            </Items>
            <Buttons>
                <ext:Button ID="ImportButton" runat="server" Text="Import Printed Badges">
                    <DirectEvents>
                        <Click OnEvent="ImportPrintedBadges">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="Values" Value="#{PrintedBadgesForm}.getForm().getValues(false)" Mode="Raw" Encode="true" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Cancel">
                    <Listeners>
                        <Click Handler="HideDialog();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:FormPanel>

    </form>
</body>
</html>
