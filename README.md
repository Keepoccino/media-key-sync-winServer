# media-key-sync-winServer

This is a .NET Core based server which allows to control the playback of the current media session on windows via Websockets. 
Its most useful in combination with a client such as the [media-key-sync-browserClient](https://github.com/Keepoccino/media-key-sync-browserClient).
That allows you to control the media session from any other device. As the name also suggests, you can use the media keys on the client to control the media session on the server.

You can use the browserClient without any downloads here: https://keepoccino.github.io/media-key-sync-browserClient/

## Why should I use this?

Imagine you have a computer which runs some application which you use to listen to music (spotify, vlc, etc... ).
Now you have another device where you work on, but cannot use to playback music, for example because you dont have the music files there or cannot install your player there.
If you want to pause the playback or switch to the next song, you will have to go to the computer and press pause/next there. 

Wouldn't it be nice to control playback directly from the device you are working from?

To solve this you can run the server on the computer where you can play music. The client only needs to open the [browserClient](https://keepoccino.github.io/media-key-sync-browserClient/).
Enter your IP and port there and you can now use the playback controls on the page. If your browser supports it, you can also use the playback keys on your keyboard even if the browser is not focused.
Also the client should show the title, artist and cover of the current song played on the server as if it was played on the client.

## Requirements

You will need to have .NET Core 5.0 or later.

Make sure to add a rule to the firewall which allows you to reach the port of the server from outside (currently the port is hardcoded to 5000).
