import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import Address from "@/interfaces/address";

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
    return api
      .post("Address/AddAddress", data)
      .then((response) => {
        response.data.data.id = response.data.id;
        this.context.commit("create", response.data.data);
        return true;
      })
      .catch(() => {
        return false;
      });
  }
}
export default Addresses;
