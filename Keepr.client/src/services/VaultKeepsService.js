import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(vaultKeepData, keep){
    const res = await api.post('api/vaultkeeps', vaultKeepData)
    keep.kept ++
    Pop.toast("Kept this Keep!", 'success')
    AppState.vaultKeeps.push(res.data)
  }

  async deleteVaultKeep(vaultKeepId){
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    Pop.toast("Keep removed!", 'success')
    let deleteIndex = AppState.vaultKeeps.findIndex(k => k.vaultKeepId == vaultKeepId)
    logger.log(deleteIndex)
    AppState.vaultKeeps.splice(deleteIndex, 1)
  }
}

export const vaultKeepsService = new VaultKeepsService()