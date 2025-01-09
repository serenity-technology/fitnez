import { computePosition, flip, shift, offset, size } from 'https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.6.12/+esm';

export function Show(reference, floating) {    
    computePosition(reference, floating, {
        placement: 'bottom',
        middleware: [
            size({
                apply({ availableWidth, availableHeight }) {
                    Object.assign(floating.style, {
                        width: `${availableWidth}px`,
                        maxHeight: `${availableHeight}px`,
                    })
                }
            }),
            offset(2),
            flip(),
            shift({ padding: 5 })            
        ],
    }).then(({ x, y }) => {
        Object.assign(floating.style, {
            left: `${x}px`,
            top: `${y}px`
        });                
    });
}