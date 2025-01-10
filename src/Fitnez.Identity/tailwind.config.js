module.exports = {
    darkMode: 'selector',
    content: {
        files: [
            '../Share.UI/**/*.razor',
            '../Share.UI/**/*.razor.cs',

            './**/*.cshtml',
            './**/*.razor',
            './**/*.razor.cs'
        ]
    },
    theme: {
        screens: {
            md: '640px',
            lg: '1008px'
        }
    },
    plugins: [
        require('@tailwindcss/typography'),
        require('@tailwindcss/forms'),
        require('@tailwindcss/aspect-ratio'),
        require('@tailwindcss/container-queries')
    ]
}