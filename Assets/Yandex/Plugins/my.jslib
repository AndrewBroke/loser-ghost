mergeInto(LibraryManager.library, {

    Hello: function () {
      window.alert("Hello, world!");
    },

    ShowAdv: function(){
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                  // some action after close
                  console.log("The Add closed ------------");
                  myGameInstance.SendMessage("Yandex", "ResumeGame")
                },
                onError: function(error) {
                  // some action on error
                },
                onOpen: function() {
                    myGameInstance.SendMessage("Yandex", "PauseGame")
                }

            }
        })
    }
  
  });