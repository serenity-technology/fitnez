// get scrollbar with
export function scrollbarWith(element) {
    let size = element.offsetWidth - element.clientWidth;
    return size;
}