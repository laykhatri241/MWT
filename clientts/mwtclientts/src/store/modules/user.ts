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
      .post("/SignUp", data)
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
  public async login(data: any): Promise<any> {
    return api
      .post("/Login", data)
      .then((response) => {
        console.log(response);
        // if (response.data.accessToken) {
        //   localStorage.setItem("user", JSON.stringify(response.data));
        // }
        //return response.data;
        //this.context.commit("login  hdjsahd Success", response.data.data)
        //return Promise.resolve(user);
        return response;
      })
      .catch(() => {
        return false;
      });
  }
  @Action
  public async CheckUsername(data: any): Promise<any> {
    return api
      .post("/CheckUsername", data)
      .then((response) => {
        console.log(response);

        //this.context.commit("CheckUsername", response.data.data);
        return true;
      })
      .catch(() => {
        return false;
      });
  }
}
export default Users;
