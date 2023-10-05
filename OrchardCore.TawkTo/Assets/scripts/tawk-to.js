var pid = document.currentScript.getAttribute('pid');
var wid = document.currentScript.getAttribute('wid');
var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
window.addEventListener('scroll', startTawkTo);
window.addEventListener('click', startTawkTo);
function startTawkTo() {
    window.removeEventListener('scroll', startTawkTo);
    window.removeEventListener('click', startTawkTo);
    var s1 = document.createElement("script");
    s1.async = true;
    s1.src = `https://embed.tawk.to/${pid}/${wid}`;
    s1.charset = 'UTF-8';
    s1.setAttribute('crossorigin', '*');
    document.head.append(s1);
}