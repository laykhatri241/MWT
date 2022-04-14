import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import User from "@/interfaces/user";

@Module({ namespaced: true })
class Users extends VuexModule {
  public user: User = {
    Fullname: "",
    id: 0,
    Username: "",
    Password: "",
    Role: 0,
  };

  @Action
  public async createUser(data: User): Promise<boolean> {
    return api
      .post('/SignUp', data)
      .then((response) => {
        response.data.data.id = response.data.id;
        this.context.commit("create", response.data.data);
        return true;
      })
      .catch(() => {
        return false;
      });
  } 
  @Action
  public async login(data: User): Promise<boolean> {
    return api
      .post('/Login', data)
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
export default Users;
