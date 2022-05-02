<template>
  <v-navigation-drawer
    v-model="drawer1"
    app
    color="#000000"
    mini-variant
    mini-variant-width="110"
    right
  >
    <v-avatar class="d-block text-center mx-auto mt-4 mb-10" size="80">
      <!-- <v-btn class="ma-2" outlined large fab color="white" to="/profile">
        SH</v-btn
      > -->
      <v-menu offset-y>
        <template v-slot:activator="{ attrs, on }">
          <span style="cursor: pointer" v-bind="attrs" v-on="on">
            <v-chip
              link
              class="d-block text-center mx-auto mt-4 mb-10"
              size="80"
            >
              <v-badge>
                <v-list-item-avatar s>
                  <v-img
                    src="https://randomuser.me/api/portraits/women/81.jpg"
                  />
                </v-list-item-avatar>
              </v-badge>
              <!-- <span class="ml-3">{{ Username }}</span> -->
            </v-chip>
          </span>
        </template>

        <v-list width="250" class="py-0">
          <v-list-item>
            <v-list-item-avatar>
              <img src="https://randomuser.me/api/portraits/women/81.jpg" />
            </v-list-item-avatar>

            <v-list-item-content>
              <v-list-item-title>{{ Username }}</v-list-item-title>
              <v-list-item-subtitle>Logged In</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
          <v-divider />
          <v-list-item link to="/profile">
            <v-list-item-icon>
              <v-icon>mdi-account</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Update Profile </v-list-item-title>
          </v-list-item>
          <v-list-item link to="changepassword">
            <v-list-item-icon>
              <v-icon>mdi-key</v-icon>
            </v-list-item-icon>
            <v-list-item-title> Change Password </v-list-item-title>
          </v-list-item>
          <v-list-item link @click="handleLogout()">
            <v-list-item-icon>
              <v-icon>mdi-logout</v-icon>
            </v-list-item-icon>
            <v-list-item-title> Logout </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-avatar>
    <!-- <span class="ml-3">{{ Username }}</span> -->
    <v-card flat color="#6F0DFF" class="rounded-xl mx-4 py-10 text-center top">
      <v-btn icon dark class="mb-5">
        <v-icon>fas fa-sort-amount-up-alt</v-icon>
      </v-btn>
      <span class="white--text">Create<br />Order</span>
    </v-card>

    <div
      style="
        position: absolute;
        bottom: 20px;
        margin-left: auto;
        margin-right: auto;
        left: 0;
        right: 0;
        text-align: center;
      "
    >
      <v-btn dark class="changewith mb-3">
        <v-icon class="">mdi-bell</v-icon> </v-btn
      ><br />
      <v-badge bordered color="#6F0DFF" dot overlap>
        <v-btn dark class="changewith">
          <v-icon class="">mdi-android-messages</v-icon>
        </v-btn>
      </v-badge>
    </div>
  </v-navigation-drawer>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import User from "@/interfaces/user";
const users = namespace("user");
@Component
export default class RightSideBar extends Vue {
  Username = localStorage.getItem("UserFullName");
  selectedItem = 0;
  drawer1 = null;
  @users.Action
  public GetMyUser!: (data: any) => Promise<any>;

  public GetUser(): void {}
  handleLogout() {
    localStorage.clear();

    this.$router.push("/");
  }
}
</script>

<style>
.v-btn:not(.v-btn--round).v-size--default.changewith {
  height: 45px !important;
  min-width: 30px !important;
  padding: 0 12px;
}
.top {
  margin-top: 150px;
}
</style>
