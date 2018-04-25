# thoth-gui
Experimental GUI front-end for Thoth with Eto.Forms

Still a little unstable.

Downloads here: https://github.com/starrtnow/thoth-gui/releases/download/0.0.2/release.zip

# Usage

![Usage Example](https://github.com/starrtnow/thoth-gui/raw/master/example.png)

update:

Apparently it's not as intuitive as I thought. Basically the point is to take a bunch of split up chapters on different pages (or on the same page, that's fine as well, just more trivial) and download them into an epub. 

For example, the image above is from here (https://www.baka-tsuki.org/project/index.php?title=Tsukumodo_Antique_Shop). I highlighted the the urls "Prologue", "Coincidence", "Statue", etc. under Volume 1. Then I pressed ctrl+c, then went to the program and pressed ctrl+v. 

There are now two user-friendly ways to input URLs to be downloaded.

1) You can simply copy links from your browser and paste it. The program will automatically detect that you've copied HTML content and parse the text as urls.

2) You can manually input urls into a text box

For covers, no more messing about with urls. Just copy a bitmap image and paste it.

# Binaries

Binaries can be found on the releases page as usual. Thoth-gui is made with Eto.forms, which has multiple backends for every platform. For now, the binaries I release will use winforms, so they will not work on Linux and OSX (probably). I will work later to produce binaries with a GTK backing for cross-platform use, but you can feel free to compile it yourself with whatever backend you want.
