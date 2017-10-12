=begin
Smarter Enemy AI
by Fomar0153
Version 1.0
----------------------
Notes
----------------------
Script makes enemies heal the most injured person
and you can set it up so they don't buff/debuff a target
who is already buffed/debuffed over a target who isn't.
----------------------
Instructions
----------------------
Use this notetag on the skills:
<state_ai x>
and it will prioritise targets without state x
----------------------
Known bugs
----------------------
None
=end

class Game_Action
  #--------------------------------------------------------------------------
  # * Targets for Opponents
  #--------------------------------------------------------------------------
  alias ai_targets_for_opponents targets_for_opponents
  def targets_for_opponents
    if !@item.object.is_a?(RPG::Skill) or (@item.object.is_a?(RPG::Skill) and @item.object.state_ai == 0)
      @subject.is_a?(Game_Actor) or !(item.for_friend? and item.for_one?)
      return ai_targets_for_opponents
    end
    t = opponents_unit.alive_members
    t = t.delete_if { |a| a.state?(@item.object.state_ai) }
    return [opponents_unit.alive_members.sample] if t.size == 0
    [t.sample]
  end
  #--------------------------------------------------------------------------
  # * Targets for Friends
  #--------------------------------------------------------------------------
  alias ai_targets_for_friends targets_for_friends
  def targets_for_friends
    return [subject] if item.for_user?
    if !@item.object.is_a?(RPG::Skill) or @subject.is_a?(Game_Actor) or !(item.for_friend? and item.for_one?)
      return ai_targets_for_friends
    end
    if @item.object.state_ai == 0
      t = friends_unit.alive_members.shuffle.sort { |a,b| a.hp_rate <=> b.hp_rate }
      [t[0]]
    else
      t = friends_unit.alive_members
      t = t.delete_if { |a| a.state?(@item.object.state_ai) }
      return [friends_unit.alive_members.sample] if t.size == 0
      [t.sample]
    end
  end
end

class RPG::Skill
  def state_ai
    if @state_ai.nil?
      if @note =~ /<state_ai (.*)>/i
        @state_ai = $1.to_i
      else
        @state_ai = 0
      end
    end
    @state_ai
  end
end