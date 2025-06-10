The PoC was done to prove that we can use applications made in unity as web plugins or be added on top of other web applications and games.

### Important Things to Note
- On the time of doing the PoC, the project should use the built-in render pipeline and not other pipelines like URP and HDRP. Using them can have some classes or libraries that might be affecting the ability of the app to be transparent.

### Setting up the Project
- Create the project with built-in render pipeline.
- Change the target platform to WebGL
- On scene, select the camera and set the following settings
	- Clear Flags: Solid Color
	- Background: any color with 0 alpha
	- Rendering Path: Forward
- Create a new jslib file and put it on Plugins folder
```jslib
// TransparentBackground.jslib
var LibraryGLClear = {  
    glClear: function(mask) {  
        if (mask === 0x00004000) {  
            var v = GLctx.getParameter(GLctx.COLOR_WRITEMASK);  
            if (!v[0] && !v[1] && !v[2] && v[3]) {  
                // We are trying to clear alpha only -- skip.  
                return;  
            }  
        }  
        GLctx.clear(mask);  
    }  
};  
  
mergeInto(LibraryManager.library, LibraryGLClear);
```
This .jslib file customizes how the alpha channel is cleared in WebGL to handle caseswhere only alpha clearing is requested but RGB channels are disabled. It helps prevent unintended or unnecessary clear calls that might affect rendering transparency or performance.

- After doing the things above you can start the build.
- After the build, look for the index.html file and change the following
	- Look for a canvas with id="unity-canvas"
	- add a css style with background: transparent
	
> The last part should be done using a custom index template file. So whenever you make a new build, you don't need to redo it as changes on the actual file in the builds folder is reset every re-build.
