import{_ as b,o as k,w as A,c as _,P as l,x as p,l as m,a as n,b as o,i as g,s as K,t as v,F,r as S,d as s,e as f,y as B,z as D,g as c,h as u,A as h,p as N,j as P}from"./index.af55ce0a.js";const j={setup(){const a=B(),r=D();async function y(){try{const t=a.params.vaultId;await p.getVaultById(t)}catch(t){m.error(t),l.error(t.message),r.push({name:"Home"})}}async function e(){try{const t=a.params.vaultId;await p.getVaultKeeps(t)}catch(t){m.error(t),l.error(t.message)}}return k(()=>{e()}),A(()=>{a.params.vaultId&&(y(),e())}),{route:a,account:_(()=>h.account),vault:_(()=>h.activeVault),vaultKeeps:_(()=>h.vaultKeeps),async deleteVault(t){try{await l.confirm("Delete this Vault?","Are you sure you want to delete this Vault? You won't be able to revert this.","Delete","question")&&(await p.deleteVault(t),r.push({name:"Account"}))}catch(d){m.error(d),l.error(d.message)}}}}},M=a=>(N("data-v-7567d43f"),a=a(),P(),a),R={key:0,class:"container-fluid p-3"},E={class:"header d-flex justify-content-center"},T={key:0,type:"button",class:"overlayTopRight"},q=["src","alt"],z={class:"overlay mb-0 text-center"},H=M(()=>o("br",null,null,-1)),L={class:"d-flex justify-content-center"},Y={class:"fs-1 m-0 mb-5 p-0"},G={class:"bricks"},J={class:"delete fs-3"};function O(a,r,y,e,t,d){const I=c("VaultKeepCard"),w=c("ActiveKeep"),i=c("Modal"),x=c("CreateKeepForm"),C=c("CreateVaultForm");return e.vault.id?(u(),n("div",R,[o("div",E,[e.account.id==e.vault.creator.id?(u(),n("div",T,[o("i",{class:"fs-1 mdi mdi-delete-outline",title:"Delete Vault",onClick:r[0]||(r[0]=V=>e.deleteVault(e.vault.id))})])):g("",!0),o("img",{class:"vaultImg",src:e.vault.img,alt:e.vault.name},null,8,q),o("p",z,[K(v(e.vault.name)+" ",1),H,K(" by "+v(e.vault.creator.name),1)])]),o("section",L,[o("p",Y,v(e.vaultKeeps.length)+" Keeps",1)]),o("section",G,[(u(!0),n(F,null,S(e.vaultKeeps,V=>(u(),n("div",J,[s(I,{vaultKeep:V},null,8,["vaultKeep"])]))),256))]),s(i,{id:"activeKeep"},{default:f(()=>[s(w)]),_:1}),s(i,{id:"createKeep"},{default:f(()=>[s(x)]),_:1}),s(i,{id:"createVault"},{default:f(()=>[s(C)]),_:1})])):g("",!0)}const U=b(j,[["render",O],["__scopeId","data-v-7567d43f"]]);export{U as default};
