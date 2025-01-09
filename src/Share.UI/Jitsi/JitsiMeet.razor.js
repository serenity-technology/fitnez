export function setup(parentNode) {
    const domain = 'meet.serenity.technology';
    const options = {
        roomName: 'JitsiMeetAPIExample',
        width: parentNode.width,
        height: parentNode.height,
        parentNode: parentNode,
        lang: 'en'
    };
    const api = new JitsiMeetExternalAPI(domain, options);
}