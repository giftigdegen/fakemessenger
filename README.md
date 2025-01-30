Fake Messenger - A Lightweight, Self-Contained WebView App


Fake Messenger is a standalone, portable WebView-based app designed to provide a clean, distraction-free Messenger experience without forced Edge browser redirection.  

Why I Made This  
I wanted hands-on experience with C# development, despite having no prior formal or informal training in C# or .NET applications. Throughout the process, I used ChatGPT as my guide, learning as I went.  
 
The latest Messenger desktop app is just a WebView wrapper, but it forces all links to open in Microsoft Edge, regardless of your default browser settings—with no option to change this.  

I figured it shouldn’t be that hard to create a lightweight, standalone applet that:  
✅ Self-unpacks and runs from a single executable  
✅ Uses very little memory  
✅ Respects your default browser for all links  
✅ Provides an uncluttered Messenger experience  
✅ Runs without installation or registry changes  

More Than Just Messenger  
Although I built this specifically for Messenger, with just a few tweaks to the code, it could easily be a portable Discord applet, a wrapper for any web-based service, or even a dedicated web kiosk for a specific website.  

Key Features  
WebView2-based – Uses Microsoft Edge’s WebView2 for smooth performance.  
Portable & Self-Contained – No installation required; just run the .exe.  
Automatic Unpacking – Extracts only the necessary files on first run.  
Minimal Memory Usage – Runs efficiently with minimal system impact.  
Respects Your Default Browser – External links open where you want them to.  
Focused, No Extra Clutter – Just the website you choose, nothing else.  


This project was not only about solving a real annoyance but also about learning C# from scratch. With ChatGPT as my tutor, I built something functional while deepening my understanding of .NET development.  

If you’re looking for a simple, no-nonsense web app wrapper, this might be exactly what you need. 🚀
