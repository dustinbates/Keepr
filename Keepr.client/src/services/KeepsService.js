import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

  async getKeeps(){
    const res = await api.get('api/keeps')
    logger.log('[GOT KEEPS]', res.data)
    AppState.keeps = res.data
  }

  async getProfileKeeps(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log('[GOT PROFILE KEEPS]', res.data)
    AppState.profileKeeps = res.data
  }

  setActiveKeep(keepId){
    AppState.activeKeep = AppState.keeps.find(k => k.id == keepId)
  }

  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    AppState.keeps.push(res.data)
  }


}

export const keepsService = new KeepsService()