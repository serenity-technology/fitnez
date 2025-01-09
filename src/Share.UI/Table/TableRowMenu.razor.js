import { computePosition, flip, shift, offset } from 'https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.6.3/+esm';

export function Show(referenceElement, floatingElement) {

    computePosition(referenceElement, floatingElement, {
        placement: 'bottom',
        middleware: [offset(5), flip(), shift({ padding: 5 })],
    }).then(({ x, y }) => {
        Object.assign(floatingElement.style, {
            left: `${x}px`,
            top: `${y}px`,
        });
    });
}