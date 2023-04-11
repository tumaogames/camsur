mergeInto(LibraryManager.library, {
  OpenOAuthInExternalTab: function (url, callback) {
    var urlString = UTF8ToString(url);
    var callbackString = UTF8ToString(callback);

    var child = window.open(urlString, "Popup Window", "width=600,height=600");
    var interval = setInterval(function() {
        try {
            // When redirected back to redirect_uri
            if (child.location.hostname === location.hostname) {
                clearInterval(interval) // Stop Interval
                
                // // Auth Code Flow -- Not used due to relative complexity
                // const urlParams = new URLSearchParams(child.location.search);
                // const authCode = urlParams.get('code');
                // console.log("Auth Code: " + authCode.toString());
                // console.log("Callback: " + callbackString);
                // window.unityInstance.SendMessage('Auth', callbackString, authCode);

                // Implicit Flow
                var fragmentString = child.location.hash.substr(1);
                var fragment = {};
                var fragmentItemStrings = fragmentString.split('&');
                for (var i in fragmentItemStrings) {
                    var fragmentItem = fragmentItemStrings[i].split('=');
                    if (fragmentItem.length !== 2) {
                        continue;
                    }
                    fragment[fragmentItem[0]] = fragmentItem[1];
                }
                var accessToken = fragment['access_token'] || '';

			fetch('https://www.googleapis.com/oauth2/v3/userinfo?access_token=' + accessToken, {
    				method: 'GET',
    				headers: {
        			'Accept': 'application/json',
    			},
			})
			.then(response => response.json())
			.then(response => {
				var res = JSON.stringify(response);
				console.log("access_token: " + accessToken);
                  	console.log("access_token: log " + response.name);
				myGameInstance.SendMessage('GoogleManager', 'callbackFunctionName', res);
			}
			);
			child.close();
            }
        }
        catch(e) {
            // Child window in another domain
            console.log("Still logging in ...");
        }
    }, 50);
  }
});
