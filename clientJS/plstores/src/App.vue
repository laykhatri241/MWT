<template>
  <div id="app">
    <v-app id="inspire">
      <v-app-bar
        :clipped-left="$vuetify.breakpoint.lgAndUp"
        app
        color="primary"
        dark
      >
        <!--      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />-->

        <v-toolbar-title style="width: 350px">
          <a href="/" class="white--text" style="text-decoration: none"
            ><v-icon>mdi-basket</v-icon>&nbsp;PNLStores</a
          >
        </v-toolbar-title>
        <v-text-field
          flat
          solo-inverted
          hide-details
          prepend-inner-icon="mdi-magnify"
          label="Search"
          class="hidden-sm-and-down pl-10 ml-4"
        />
        <v-spacer />
       
        <div v-if="isuserloggedin">
         

          <v-btn icon>
            <v-badge content="2" value="2" color="green" overlap>
              <v-icon>mdi-bell</v-icon>
            </v-badge>
          </v-btn>
          <v-btn href="/cart" icon>
            <v-badge content="2" value="2" color="green" overlap>
              <v-icon>mdi-cart</v-icon>
            </v-badge>
          </v-btn>
           <v-btn icon @click="logout">
            <v-icon>mdi-logout</v-icon>
          </v-btn>
        </div>
        <div v-else>
 
           <v-btn icon @click="showdialog = true"> Login/ Signup
           </v-btn>
          <login
            v-if="showdialog"
            @showpopup="showpopup"
            @userloggedin="userloggedin"
          />
        </div>
        <v-switch @click="darkMode" />
      </v-app-bar>
      <v-content>
        <v-bottom-navigation :value="activeBtn" color="primary" horizontal>
          <a href="/" class="v-btn">
            <span>Home</span>
          </a>
          <v-menu open-on-hover offset-y>
            <template v-slot:activator="{}">
              <v-btn>
                <span>Shop</span>
              </v-btn>
            </template>
            <v-card class="mx-auto" max-width="344" outlined>
              <v-list-item v-for="(item, index) in items" :key="index">
                <v-list-item-title>{{ item.title }}</v-list-item-title>
              </v-list-item>
            </v-card>
          </v-menu>
          <a href="/product" class="v-btn">
            <span>Product</span>
          </a>
          <v-btn href="/blog">
            <span>Blog</span>
          </v-btn>
        </v-bottom-navigation>
      </v-content>
      <router-view />
      <v-footer :padless="true">
        <v-card
          flat
          tile
          width="100%"
          class="secondary white--text text-center"
        >
          <v-card-text>
            <v-btn class="mx-4 white--text" icon>
              <v-icon size="24px">mdi-home</v-icon>
            </v-btn>
            <v-btn class="mx-4 white--text" icon>
              <v-icon size="24px">mdi-email</v-icon>
            </v-btn>
            <v-btn class="mx-4 white--text" icon>
              <v-icon size="24px">mdi-calendar</v-icon>
            </v-btn>
            <v-btn class="mx-4 white--text" icon>
              <v-icon size="24px">mdi-delete</v-icon>
            </v-btn>
          </v-card-text>

          <v-card-text class="white--text pt-0">
            For every sale you miss because you're too enthusiastic, you will
            miss a hundred because you're not enthusiastic enough.” “Every
            choice you make has an end result.” "Your attitude, not your
            aptitude, will determine your altitude." .
          </v-card-text>

          <v-divider></v-divider>

          <v-card-text class="white--text">
            {{ new Date().getFullYear() }} — <strong>@ CopyRight</strong>
          </v-card-text>
        </v-card>
      </v-footer>
    </v-app>

    
  </div>
</template>

<script>
import Login from "@/components/Login.vue";
export default {
  name: "App",
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
      isuserloggedin: false,
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
    userloggedin(userexist) {
      this.isuserloggedin = userexist;
    },
    logout(){
          localStorage.removeItem('userid');
          localStorage.removeItem('userrole');
          localStorage.removeItem('logintoken');
          this.$router.push("/")
    }
  },
  computed: {
    theme() {
      return this.$vuetify.theme.dark ? "dark" : "light";
    },
  },
};
</script>
