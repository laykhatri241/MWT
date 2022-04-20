<template>

<div id="app">
  <!-- nav bar -->
  
  <!-- complete nav bar -->
    <v-app>
        <v-dialog  persistent v-model="dialog" 
         max-width="600px" min-width="360px">
            <div>
                <v-tabs v-model="tab" show-arrows background-color="primary accent-1" icons-and-text dark grow>
                    <v-tabs-slider color="primary darken-4"></v-tabs-slider>
                    <v-tab v-for="i in tabs" :key="i.uniqueId">
                        <v-icon large>{{ i.icon }}</v-icon>
                        <div class="caption py-1">{{ i.name }}</div>
                    </v-tab>
                    <v-tab-item>
                        <v-card class="px-4">
                            <v-card-text>
                                <v-form ref="loginForm" v-model="valid" lazy-validation>
                                    <v-row>
                                        <v-col cols="12">
                                            <v-text-field v-model="loginusername" :rules="[rules.required]" label="UserName" required></v-text-field>
                                        </v-col>
                                        <v-col cols="12">
                                            <v-text-field v-model="loginPassword" :append-icon="show1?'eye':'eye-off'" :rules="[rules.required, rules.min]" :type="show1 ? 'text' : 'password'" name="input-10-1" label="Password" hint="At least 8 characters" counter @click:append="show1 = !show1"></v-text-field>
                                        </v-col>
                                        <v-col class="d-flex" cols="12" sm="6" xsm="12">
                                        </v-col>
                                        <v-spacer></v-spacer>
                                        <v-col class="d-flex" cols="12" sm="3" xsm="12" align-end>
                                            <v-btn large block :disabled="!valid" color="success" @click="validate()" href="companyregister"> Login </v-btn>
                                            </v-col>
                                            <v-col class="d-flex" cols="12" sm="3" xsm="12" align-end>
                                             <v-btn large  color="success" @click="showpopup"> Close </v-btn>
                                            </v-col>
                                             
                                    </v-row>
                                </v-form>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                    <v-tab-item>
                        <v-card class="px-4">
                            <v-card-text>
                                <v-form ref="registerForm" v-model="valid" lazy-validation>
                                    <v-row>
                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="fullName" :rules="[rules.required]" label="Full Name" maxlength="20" required></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="userName" :rules="[rules.required]" label="User Name" maxlength="20" required></v-text-field>
                                        </v-col>
                                        <v-col cols="12">
                                            <v-text-field v-model="password" :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" :rules="[rules.required, rules.min]" :type="show1 ? 'text' : 'password'" name="input-10-1" label="Password" hint="At least 8 characters" counter @click:append="show1 = !show1"></v-text-field>
                                        </v-col>
                                        <v-col cols="12">
                                            <v-text-field block v-model="verify" :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'" :rules="[rules.required, passwordMatch]" :type="show1 ? 'text' : 'password'" name="input-10-1" label="Confirm Password" counter @click:append="show1 = !show1"></v-text-field>
                                        </v-col>
                                        <v-spacer></v-spacer>
                                        <v-col class="d-flex ml-auto" cols="12" sm="3" xsm="12">
                                            <v-btn x-large  color="success" @click="validatesignup()">Register</v-btn>
                                        </v-col>
                                           <v-col class="d-flex" cols="12" sm="3" xsm="12" >
                                             <v-btn large  color="success" @click="showpopup"> Close </v-btn>
                                            </v-col>
                                    </v-row>
                                </v-form>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                </v-tabs>
            </div>
        </v-dialog>
    </v-app>
</div>
</template>
<script>
import axios from "axios";
export default({

  computed: {
    passwordMatch() {
      return () => this.password === this.verify || "Password must match";
    }
  },
  methods: {
    async validate() {
        
      if(this.$refs.loginForm.validate() ){
           const login=await axios.post("https://localhost:5001/Account/Login",{"Username":this.userName,"Password":this.password})
 localStorage.setItem('logintoken',JSON.stringify(login.data));
        var getdata = JSON.parse(localStorage.getItem('logintoken'));
        console.log(getdata);
        console.log(login)
      }
    },

    
      async validatesignup() {
        console.log("hello");
      if (this.$refs.registerForm.validate()) {
        // submit form to server/API here...
        const signup=await axios.post("https://localhost:5001/Account/SignUp",{"Fullname":this.fullName,"Username":this.userName,"Password":this.password," Role":3})
        console.log(signup)
       
      }
    },
    showpopup(){
        
             this.$emit("showpopup", false);
    },

    fetch(){

    },
    reset() {
      this.$refs.form.reset();
    },
    resetValidation() {
      this.$refs.form.resetValidation();
    }
  },
  data: () => ({
  
    dialog: true,
    tab: 0,
    tabs: [
        {name:"Login", icon:"mdi-account"},
        {name:"Register", icon:"mdi-account-outline"}
    ],
    valid: true,
    
    fullName: "",
    userName: "",
    password: "",
    verify: "",
    loginPassword: "",
    loginusername: "",
    
    // emailRules: [
    //   v => !!v || "Required",
    //   v => /.+@.+\..+/.test(v) || "E-mail must be valid"
    // ],

    show1: false,
    rules: {
      required: value => !!value || "Required.",
      min: v => (v && v.length >= 8) || "Min 8 characters"
    }
    
  })
  

});
   

</script>
