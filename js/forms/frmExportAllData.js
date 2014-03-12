function ExportAllData() {
    showPleaseWait();
    $.ajax({
      type: "POST",
      url: "serverMSysMan.asmx/TImportExportWebConnector_ExportAllTables",
      data: JSON.stringify({
            }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function(data, status, response) {
        result = JSON.parse(response.responseText);
        if (result['d'] != 0)
        {
            // using download feature from HTML5, see http://caniuse.com/download
            // and http://www.w3.org/TR/html/links.html#downloading-resources
            var pom = document.createElement('a');
            pom.setAttribute('href', 'data:application/gzip;base64,' + encodeURIComponent(result['d']));
            pom.setAttribute('download', "exportedDatabase.yml.gz");
            
            // For Mozilla we need to add the link, otherwise the click won't work
            // see https://support.mozilla.org/de/questions/968992
            document.body.appendChild(pom);
            
            pom.click();
        }
        else
        {
            console.debug("something went wrong");
        }
        hidePleaseWait();
      },
      error: function(response, status, error) {
        console.debug(error);
        console.debug(JSON.stringify(response.responseJSON));
        alert("Server error, please try again later");
        hidePleaseWait();
      },
      fail: function(msg) {
        console.debug(msg);
        alert("Server failure, please try again later");
        hidePleaseWait();
      }
    });
}

jQuery(document).ready(function() {

    $('#btnExportData').on('click', function (e) {
        ExportAllData();
    });
});

function showPleaseWait() {
    $('#myModal').modal();
}
function hidePleaseWait() {
    $('#myModal').modal('hide');
}
