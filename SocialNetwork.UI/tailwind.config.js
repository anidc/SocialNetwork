/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'primary': '#2B2D42',
        'secondary': '#8D99AE',
        'tertiary': '#EDF2F4',
      },
      screens:{
        '3xl': '2000px'
      }
    },
  },
  plugins: [],
}