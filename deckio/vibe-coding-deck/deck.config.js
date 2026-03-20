import CoverSlide from './src/slides/CoverSlide.jsx'
import DefinitionSlide from './src/slides/DefinitionSlide.jsx'
import ToolingSlide from './src/slides/ToolingSlide.jsx'
import AgentModesSlide from './src/slides/AgentModesSlide.jsx'
import TimsCaseSlide from './src/slides/TimsCaseSlide.jsx'
import PrerequisitesSlide from './src/slides/PrerequisitesSlide.jsx'
import RunTheAppSlide from './src/slides/RunTheAppSlide.jsx'
import FeatureIdeasSlide from './src/slides/FeatureIdeasSlide.jsx'
import GoWildSlide from './src/slides/GoWildSlide.jsx'
import { GenericThankYouSlide as ThankYouSlide } from '@deckio/deck-engine'

export default {
  id: 'vibe-coding-deck',
  title: 'Vibe Coding with GitHub Copilot CLI',
  subtitle: 'Vibe Coding Hackathon',
  description: 'Intro, capabilities, Tim\'s concrete case, hack expectations, prerequisites, and kickoff plan.',
  meta: {
    seededTemplate: true,
    contentStatus: 'sample',
    contextPolicy: 'ignore-sample-content-until-user-replaces-it',
  },
  icon: '🎴',
  accent: '#06b6d4',
  theme: 'dark',
  appearance: 'dark',
  order: 1,
  slides: [
    CoverSlide,
    DefinitionSlide,
    ToolingSlide,
    AgentModesSlide,
    TimsCaseSlide,
    PrerequisitesSlide,
    RunTheAppSlide,
    FeatureIdeasSlide,
    GoWildSlide,
    ThankYouSlide,
  ],
}
