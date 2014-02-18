function Init() {

// Partner Status
    $.ajax({
      type: "POST",
      url: "serverMPartner.asmx/TPartnerCacheableWebConnector_GetCacheableTable",
      data: JSON.stringify({
            'ACacheableTable': 'PartnerStatusList',
            'AHashCode': '',
            }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function(data, status, response) {
        result = JSON.parse(response.responseText);
        //console.debug(JSON.stringify(response.responseText));
        //console.debug(JSON.stringify(result));
        if (result['d'] != 0)
        {
            //console.debug(JSON.stringify(result['d']));
            //console.debug(JSON.stringify(JSON.parse(result['d'])));

            SearchResult = JSON.parse(result['d'])[1];

            ParsedSearchResult = new Array();

            options = "";
            for (index = 0; index < SearchResult.length; ++index) {
                options += "<option value='" + SearchResult[index]["p_status_code_c"] + "'>" +
                    SearchResult[index]["p_status_code_c"] + "</option>";
            }
            $("#partnerstatus").html(options);
        }
        else
        {
            alert("something went wrong");
        }
      },
      error: function(response, status, error) {
        console.debug(error);
        console.debug(JSON.stringify(response.responseJSON));
        alert("Server error, please try again later");
      },
      fail: function(msg) {
        console.debug(msg);
        alert("Server failure, please try again later");
      }
    });

// Country
    $.ajax({
      type: "POST",
      url: "serverMCommon.asmx/TCommonCacheableWebConnector_GetCacheableTable",
      data: JSON.stringify({
            'ACacheableTable': 'CountryList',
            'AHashCode': '',
            }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function(data, status, response) {
        result = JSON.parse(response.responseText);
        //console.debug(JSON.stringify(response.responseText));
        //console.debug(JSON.stringify(result));
        if (result['d'] != 0)
        {
            //console.debug(JSON.stringify(result['d']));
            //console.debug(JSON.stringify(JSON.parse(result['d'])));

            SearchResult = JSON.parse(result['d'])[1];

            ParsedSearchResult = new Array();

            options = "<option></option>";
            for (index = 0; index < SearchResult.length; ++index) {
                options += "<option value='" + SearchResult[index]["p_country_code_c"] + "'>" +
                    SearchResult[index]["p_country_name_c"] + "</option>";
            }
            $("#country").html(options);
        }
        else
        {
            alert("something went wrong");
        }
      },
      error: function(response, status, error) {
        console.debug(error);
        console.debug(JSON.stringify(response.responseJSON));
        alert("Server error, please try again later");
      },
      fail: function(msg) {
        console.debug(msg);
        alert("Server failure, please try again later");
      }
    });

// Location Type    
    $.ajax({
      type: "POST",
      url: "serverMPartner.asmx/TPartnerCacheableWebConnector_GetCacheableTable",
      data: JSON.stringify({
            'ACacheableTable': 'LocationTypeList',
            'AHashCode': '',
            }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function(data, status, response) {
        result = JSON.parse(response.responseText);
        //console.debug(JSON.stringify(response.responseText));
        //console.debug(JSON.stringify(result));
        if (result['d'] != 0)
        {
            //console.debug(JSON.stringify(result['d']));
            //console.debug(JSON.stringify(JSON.parse(result['d'])));

            SearchResult = JSON.parse(result['d'])[1];

            ParsedSearchResult = new Array();

            options = "";
            for (index = 0; index < SearchResult.length; ++index) {
                options += "<option value='" + SearchResult[index]["p_code_c"] + "'>" +
                    SearchResult[index]["p_code_c"] + "</option>";
            }
            $("#locationtype").html(options);
        }
        else
        {
            alert("something went wrong");
        }
      },
      error: function(response, status, error) {
        console.debug(error);
        console.debug(JSON.stringify(response.responseJSON));
        alert("Server error, please try again later");
      },
      fail: function(msg) {
        console.debug(msg);
        alert("Server failure, please try again later");
      }
    });
}
