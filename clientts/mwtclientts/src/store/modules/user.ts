import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import User from "@/interfaces/user";

@Module({ namespaced: true })
class Users extends VuexModule {
  public user: User = {
    Fullname: "hhh",
    id: 0,
    Username: "",
    Password: "",
    Role: 0,
  };

  @Action
  public async createUser(data: User): Promise<boolean> {
    console.log(data);

    return api

      .post('/SignUp', {
        data,
      })
      .then((response) => {
        debugger;
        response.data.data.id = response.data.id;
        this.context.commit("create", response.data.data);
        return true;
      })
      .catch(() => {
        return false;
      });
  }
  @Action
  public async Check(data: User): Promise<boolean> {
    console.log(data);

    return api

      .post('/CheckUsername', {
        data,
      })
      .then((response) => {
        debugger;
        response.data.data.id = response.data.id;
        this.context.commit("create", response.data.data);
        return true;
      })
      .catch(() => {
        return false;
      });
  }
}
export default Users;
