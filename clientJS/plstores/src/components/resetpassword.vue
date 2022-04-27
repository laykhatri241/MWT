<template>
  <v-container>
     <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label=" old password"
          hint=" old password"
          persistent-hint
          v-model="OldPass"
          outlined
        >
        </v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="password"
          hint="password"
          persistent-hint
          v-model="NewPass"
          

          outlined
        >
        </v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="reenter password"
          hint="confirm password"
          v-model="verify"

          persistent-hint
          outlined
        ></v-text-field>
      </v-col>
    </v-row>
    <v-btn depressed color="primary" @click="resetpassword()">submit</v-btn>
  </v-container>
</template>

<script>
import callAPI from "@/apihelper/api";
export default {
  
  methods: {
    async resetpassword() {
      callAPI.AsyncPOST("Account/ChangePassword", {
        id: localStorage.getItem("userid"),
        OldPass:this.OldPass,
        NewPass:this.NewPass,
      }).then((data) => console.log(data));
    },
  },
  data: () => ({
    OldPass:"",
    NewPass: "",
    verify: "",
  }),
   }

</script>
