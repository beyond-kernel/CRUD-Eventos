import { Lote } from "./lote";
import { RedeSocial } from "./rede-social";
import { Palestrante } from "./palestrante";

export interface Evento {
    eventoId: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    lotes: Lote[];
    telefone: string;
    imagemUrl: string;
    email: RedeSocial[];
    palestranteEvento: Palestrante[];
}
