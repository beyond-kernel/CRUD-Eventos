import { RedeSocial } from "./rede-social";
import { Evento } from "./evento";

export interface Palestrante {
     id: number;
     nome: string;
     miniCurriculo: string;
     imagemUrl: string;
     telefone: string;
     email: string;
     redesSociais: RedeSocial[];
     palestranteEvento: Evento[];
}
