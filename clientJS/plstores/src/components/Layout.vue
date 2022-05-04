<template>
 
</template>
<script>
import Login from "./Login.vue";
export default {
  components: { Login },
  theme: {
    themes: {
      light: {
        primary: "#82b19f",
        secondary: "#f4bf1c",
        accent: "#47333c",
        error: "#dc6276",
        background: "#F6F5F4",
        formBackground: "#f7f7f7",
      },
      dark: {
        primary: "#82b19f",
        background: "#181b1f",
        formBackground: "#23272b",
      },
    },
  },

  data() {
    return {
      items: [
        { title: "T-Shirts" },
        { title: "Jackets" },
        { title: "Shirts" },
        { title: "Jeans" },
        { title: "Shoes" },
      ],
      activeBtn: 1,
      showdialog: false,
    };
  },
  methods: {
    showpopup() {
      this.showdialog = false;
    },
    darkMode() {
      this.$vuetify.theme.dark = !this.$vuetify.theme.dark;
    },
  },
  computed: {
    theme() {
      return this.$vuetify.theme.dark ? "dark" : "light";
    },
  },
};
</script>
