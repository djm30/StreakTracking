/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        dark: "#131313",
        light: "#FFFFFF",
        darkGrey: "#242424",
        offGrey: "#525252",
        lightGrey: "#3B3B3C",
        accentInactive: "#404e66",
        accent: "#627EAD",
        highlight: "#54A5E0",
        correctGreen: "#2FC73E",
        wrongRed: "#E50000",
      },
    },
  },
  plugins: [],
};
