// Map c# enum
const Breakpoint = {
    Small: 1,
    Medium: 2,
    Large: 3
};
Object.freeze(Breakpoint);

// calculate TailwindCSS Breakpoint
function calcBreakpoint(width) {
    if (width < 640)
        return Breakpoint.Small;

    if (width >= 640 && width < 1008)
        return Breakpoint.Medium;

    if (width >= 1008)
        return Breakpoint.Large;
};

// some global variables
var _dotnet;
var _currentBreakpoint = calcBreakpoint(window.innerWidth);

// Add Resize
export function addResize(dotnet) {
    _dotnet = dotnet;
    _dotnet.invokeMethodAsync('OnResized', { Breakpoint: _currentBreakpoint });

    window.addEventListener("resize", onResize);
};

// Remove Resize
export function removeResize() {
    window.removeEventListener("resize", onResize);
};

// On Resize
function onResize() {
    var newBreakpoint = calcBreakpoint(window.innerWidth);

    if (_currentBreakpoint != newBreakpoint) {
        _currentBreakpoint = newBreakpoint;
        _dotnet.invokeMethodAsync('OnResized', { Breakpoint: _currentBreakpoint });
    }
};

// DarkMode
export function isDarkMode() {
    let isdark = (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) ? true : false;
    return isdark;
}