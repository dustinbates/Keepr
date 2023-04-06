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

  async setActiveKeep(keep){
    const res = await api.get(`api/keeps/${keep.id}`)
    const creatorId = AppState.account.id
    if(keep.creatorId != creatorId){
      keep.views++
    }
    AppState.activeKeep = keep
  }

  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    AppState.keeps.push(res.data)
    AppState.profileKeeps.push(res.data)
  }

  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    let deleteIndex = AppState.keeps.findIndex(k => k.id == keepId)
    AppState.keeps.splice(deleteIndex, 1)
  }


}

export const keepsService = new KeepsService()