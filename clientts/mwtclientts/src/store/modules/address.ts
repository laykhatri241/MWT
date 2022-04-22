import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import Address from "@/interfaces/address";
localStorage.getItem("UserId");
@Module({ namespaced: true })
class Addresses extends VuexModule {
  public address: Address = {
    id: 0,
    UserID: 0,
    Address1: "",
    Address2: "",
    Pincode: "",
  };

  @Action
  public async createAddress(data: Address): Promise<boolean> {
    // this.address.UserID = localStorage.getItem("UserId");
    return api

      .post("Address/AddAddress", data)

      .then((response) => {
        console.log(response);
        // this.address.UserID = localStorage.getItem("UserId");

        return true;
      })
      .catch(() => {
        return false;
      });
  }
}
export default Addresses;
