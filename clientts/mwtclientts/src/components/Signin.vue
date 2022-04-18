<template>
  <div id="c-login" class="wallpaper-register">
    <v-card class="mx-auto" outlined id="login-card">
      <div
        class="text-start v-card--material__heading mb-n6 v-sheet theme--dark elevation-6 primary pa-7"
        style="width: 90%; margin: 20px"
      >
        <div data-v-72fcb493="" class="display-3 text-center">Sign in!!</div>
        <!---->
      </div>
      <div data-v-72fcb493="" class="text-center mb-4">
        <img
          data-v-72fcb493=""
          src="https://va-demo.okami101.io/admin/img/logo.bad423a5.svg"
          width="80"
          height="80"
        />
      </div>
      <v-card-text>
        <v-form ref="form" lazy-validation @submit.prevent="submit">
          <v-text-field
            label="Username"
            type="text"
            color="blue accent-3"
            v-model="user.Username"
            :rules="nameRules"
          />
          <v-text-field
            id="password"
            label="Password"
            v-model="user.Password"
            :append-icon="value ? 'visibility' : 'visibility_off'"
            @click:append="() => (value = !value)"
            name="password"
            :type="value ? 'password' : 'text'"
            color="blue accent-3"
          />
          <p>
            Don't have an account?
            <router-link to="/Signup">Signup here</router-link>
          </p>
          <h3 style="color: red" class="text-center mt-4">
            {{ message }}
          </h3>
          <v-btn
            class="mt-3"
            block
            color="blue accent-3"
            dark
            @click="Loginform()"
            >SIGN IN</v-btn
          >
        </v-form>
      </v-card-text>
    </v-card>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import User from "@/interfaces/user";
const users = namespace("user");
@Component
export default class AddUser extends Vue {
  public isNew?: boolean = true;
  private message: string = "";
  valid = true;
  Roles = [
    { Did: 1, DName: "User" },
    { Did: 0, DName: "Admin" },
  ];
  passwordRules = [
    (v: any) => !!v || "Password is required",
    (v: string) =>
      /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/.test(v) ||
      "Password must contain at least lowercase letter, one number, a special character and one uppercase letter",
  ];
  confirmPasswordRules = [
    (value: any) => !!value || "type confirm password",
    (value: any) =>
      value === this.user.Password ||
      "The password confirmation does not match.",
  ];
  nameRules = [(v: any) => !!v || "Value is required"];
  step = 1;
  @Prop(String)
  source!: string;
  @Prop(String)
  value!: string;
  @Prop(String)
  simpleData!: string;

  public user: User = {
    Fullname: "",
    id: 0,
    Username: "",
    Password: "",
    Role: 3,
  };

  public submitted: boolean = false;

  @users.Action
  public login!: (data: any) => Promise<any>;

  public Loginform(): void {
    if (this.user.Username && this.user.Password) {
      this.login(this.user).then(
        (data) => {
          if (data.data.content != "null") {
            this.$router.push("/Dashboard");
            localStorage.setItem;
          } else {
            this.message = "Username Or Password is incorrect!!";
          }
          //console.log(data.data.content);
        },
        (error) => {
          this.message = error;
        }
      );
    }
    //this.login({Username:this.user.Username ,Password:this.user.Password})
    //this.$router.push('/profile')
  }
}
</script>
<style scoped>
#c-login {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
#login-card {
  width: 400px;
}
.wallpaper-register {
  background: url(https://images.pexels.com/photos/32237/pexels-photo.jpg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260)
    no-repeat center center;
  background-size: cover;
  height: 100%;
  position: absolute;
  width: 100%;
}
.v-card--material__heading {
  position: relative;
  top: -40px;
  -webkit-transition: 0.3s ease;
  transition: 0.3s ease;
  z-index: 1;
}
.v-sheet {
  border-radius: 4px;
}
.theme--dark.v-sheet {
  background-color: #1e1e1e;
  border-color: #1e1e1e;
  color: #fff;
}
.v-card .v-card--material__heading {
  top: -30px;
}
</style>
