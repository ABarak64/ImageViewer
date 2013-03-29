Image Viewer
============================

A Slideshow in C#.NET 4.5 WPF of images sourced online. The images are generally found by either scraping popular websites or consuming a website's public API.

ImageViewer
------------------------
The dead-simple WPF Desktop application that allows the user to select the website to source the images from, and a 'next' button to display the next image.

ImageProviders
--------------
A C#.NET class library that contains the various classes that find image urls, all implementing a common interface for the ImageViewer. Additionally, there is an abstract factory that returns a particular ImageProvider based on the website desired.

Currently Image Providers:
* A Pinterest scraper
* A Reddit JSON API consumer

ImageProviderTests
------------------
Contains unit tests for the ImageProviders, as well as integration tests to confirm the DOM/JSON structures of the websites associated with the Image Providers still parse correctly.