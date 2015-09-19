function preloadImages() {
    if (document.images) {
        var loadedImages = new Array;
        for(arg=0;arg<preloadImages.arguments.length;arg++) {
            loadedImages[arg] = new Image();
            loadedImages[arg].src = preloadImages.arguments[arg];
        }
    }
 }
 
function img_hot (imgName) {
    if (document.images)
    {       
        document [imgName].src = "images/" + imgName + "_on.gif"
    }
}
function img_cool (imgName) {
    if (document.images)
    {       
        document [imgName].src = "images/" + imgName + ".gif"
    }
}