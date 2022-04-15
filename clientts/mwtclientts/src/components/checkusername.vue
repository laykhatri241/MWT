<template>
  <div>
    <v-text-field
      label="Email"
      name="Email"
      v-model="user.Username"
      type="text"
      color="blue accent-3"
    />
    <v-btn rounded class="mt-3" @click="submitForm()" color="blue accent-3" dark>SIGN UP</v-btn>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import  User  from "@/interfaces/user";
const users = namespace("user");
@Component
export default class  extends Vue {
     public isNew?: boolean = true
    public user: User = {
    Fullname: 'hhth',
    id: 0,
    Username: 'nikunj',
    Password: 'Krunal@214',
    Role: 3,
  };
    @users.Action
  public Check!: (data: User) => Promise<boolean>;

  
    public submitForm(): void {
    
    if (this.isNew) {
      console.log("Subm");
      
      
      this.Check(this.user)
    } else {
      console.log("error");
      
    }
  }

}
</script>