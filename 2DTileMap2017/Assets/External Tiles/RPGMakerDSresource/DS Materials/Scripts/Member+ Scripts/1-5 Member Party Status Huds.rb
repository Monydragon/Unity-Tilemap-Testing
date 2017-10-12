=begin
1-5 Member Party Status Huds
by Fomar0153
Version 1.0
----------------------
Notes
----------------------
Automatically uses up all available space in the status menus.
This is a member+ script.
----------------------
Instructions
----------------------
Follow the instructions in the MembersPlus module.
----------------------
Known bugs
----------------------
None
=end
module MembersPlus

  # Set this to the number of people you want to take part in battle
  Battle_Party_Size = 1
  # When one person is in the party you can draw a portrait, it should
  # be placed in pictures and then you can notetag the actor e.g.
  # <portrait RalphPortrait>
  One_Member_Party_Use_Portrait = true
  # When 5 people are in the party their faces can sometimes overlap slightly,
  # this prevents that.
  Five_Party_Trunc_Faces = true

end

class Window_MenuStatus < Window_Selectable
#--------------------------------------------------------------------------
# * Get Item Height
#--------------------------------------------------------------------------
  def item_height
    (height - standard_padding * 2) / [$game_party.members.size, MembersPlus::Battle_Party_Size].min
  end
#--------------------------------------------------------------------------
# * Draw Item
#--------------------------------------------------------------------------
  def draw_item(index)
    actor = $game_party.members[index]
    enabled = $game_party.battle_members.include?(actor)
    rect = item_rect(index)
    draw_item_background(index)
    draw_actor_face(actor, rect.x + 1, rect.y + 1, enabled)
  if MembersPlus::One_Member_Party_Use_Portrait && $game_party.members.size == 1
    bitmap = Cache.picture(actor.actor.portrait)
    contents.blt(rect.x + 160, rect.y + line_height * 4, bitmap, bitmap.rect)
  end
  if MembersPlus::Battle_Party_Size == 5 && $game_party.members.size >= 5
    draw_actor_simple_status(actor, rect.x + 108, rect.y + line_height / 4)
  else
    draw_actor_simple_status(actor, rect.x + 108, rect.y + line_height / 2)
  end
  case [$game_party.members.size, MembersPlus::Battle_Party_Size].min
    when 1
      draw_exp_info(actor, rect.x + 4,rect.y + line_height * 4)
      6.times {|i| draw_actor_param(actor, rect.x + 4, rect.y + line_height * (9 + i), i + 2) }
    when 2
      draw_exp_info(actor, rect.x + 4,rect.y + line_height * 4)
    end
  end
#--------------------------------------------------------------------------
# * Draw Experience Information
#--------------------------------------------------------------------------
  def draw_exp_info(actor, x, y)
    s1 = actor.max_level? ? "-------" : actor.exp
    s2 = actor.max_level? ? "-------" : actor.next_level_exp - actor.exp
    s_next = sprintf(Vocab::ExpNext, Vocab::level)
    change_color(system_color)
    draw_text(x, y + line_height * 0, 156, line_height, Vocab::ExpTotal)
    draw_text(x, y + line_height * 2, 156, line_height, s_next)
    change_color(normal_color)
    draw_text(x, y + line_height * 1, 156, line_height, s1, 2)
    draw_text(x, y + line_height * 3, 156, line_height, s2, 2)
  end
#--------------------------------------------------------------------------
# * Draw Face Graphic
# enabled : Enabled flag. When false, draw semi-transparently.
#--------------------------------------------------------------------------
  def draw_face(face_name, face_index, x, y, enabled = true)
    return super(face_name, face_index, x, y, enabled) unless MembersPlus::Five_Party_Trunc_Faces
      bitmap = Cache.face(face_name)
      rect = Rect.new(face_index % 4 * 96, face_index / 4 * 96, 96, 96)
      rect.height = [rect.height,item_height - 2].min
      contents.blt(x, y, bitmap, rect, enabled ? 255 : translucent_alpha)
      bitmap.dispose
    end
  end

class Game_Party < Game_Unit
#--------------------------------------------------------------------------
# * Get Maximum Number of Battle Members
#--------------------------------------------------------------------------
  def max_battle_members
    return MembersPlus::Battle_Party_Size
    end
  end

  class RPG::Actor < RPG::BaseItem
    def portrait
      if @portrait.nil?
        if @note =~ /<portrait (.*)>/i
        @portrait = $1
        else
        @portrait = @name
        end
    end
    @portrait
    end
  end

class Window_BattleStatus < Window_Selectable
#--------------------------------------------------------------------------
# * Object Initialization
#--------------------------------------------------------------------------
  def initialize
    super(0, 0, window_width, window_height)
    refresh
    self.openness = 0
  end
#--------------------------------------------------------------------------
# * Get Digit Count
#--------------------------------------------------------------------------
  def col_max
    item_max
  end
#--------------------------------------------------------------------------
# * Get Spacing for Items Arranged Side by Side
#--------------------------------------------------------------------------
  def spacing
    0
  end
#--------------------------------------------------------------------------
# * Get Item Height
#--------------------------------------------------------------------------
  def item_height
    line_height * 4
  end
#--------------------------------------------------------------------------
# * Draw Item
#--------------------------------------------------------------------------
  def draw_item(index)
    actor = $game_party.battle_members[index]
    draw_basic_area(basic_area_rect(index), actor)
    draw_gauge_area(gauge_area_rect(index), actor)
  end
#--------------------------------------------------------------------------
# * Get Basic Area Retangle
#--------------------------------------------------------------------------
  def basic_area_rect(index)
    item_rect_for_text(index)
  end
#--------------------------------------------------------------------------
# * Get Gauge Area Rectangle
#--------------------------------------------------------------------------
  def gauge_area_rect(index)
    rect = item_rect_for_text(index)
    rect.x += 4
    rect.width -= 8
    rect
  end
#--------------------------------------------------------------------------
# * Get Gauge Area Width
#--------------------------------------------------------------------------
  def gauge_area_width
    return contents.width / item_max
  end
#--------------------------------------------------------------------------
# * Draw Basic Area
#--------------------------------------------------------------------------
  def draw_basic_area(rect, actor)
    draw_actor_face(actor, rect.x + 1, rect.y + 1, actor.alive?)
    if $data_system.opt_display_tp
      draw_actor_icons(actor, rect.x, rect.y, rect.width)
    else
    if $game_party.members.size <= 2
      draw_actor_name(actor, rect.x + 100, rect.y, rect.width - 100)
      draw_actor_icons(actor, rect.x + 100, rect.y + line_height, rect.width - 100)
    else
      draw_actor_name(actor, rect.x, rect.y, rect.width)
      draw_actor_icons(actor, rect.x, rect.y + line_height, rect.width)
      end
    end
  end
#--------------------------------------------------------------------------
# * Draw Gauge Area
#--------------------------------------------------------------------------
  def draw_gauge_area(rect, actor)
    if $game_party.members.size <= 2
      rect.x += 100
      rect.width -= 100
    end
    if $data_system.opt_display_tp
      draw_gauge_area_with_tp(rect, actor)
    else
      draw_gauge_area_without_tp(rect, actor)
    end
  end
#--------------------------------------------------------------------------
# * Draw Gauge Area (with TP)
#--------------------------------------------------------------------------
  def draw_gauge_area_with_tp(rect, actor)
    draw_actor_hp(actor, rect.x, rect.y + line_height, rect.width)
    draw_actor_mp(actor, rect.x, rect.y + line_height * 2, rect.width)
    draw_actor_tp(actor, rect.x, rect.y + line_height * 3, rect.width)
  end
#--------------------------------------------------------------------------
# * Draw Gauge Area (without TP)
#--------------------------------------------------------------------------
  def draw_gauge_area_without_tp(rect, actor)
    draw_actor_hp(actor, rect.x, rect.y + line_height * 2, rect.width)
    draw_actor_mp(actor, rect.x, rect.y + line_height * 3, rect.width)
  end
#--------------------------------------------------------------------------
# * Draw Face Graphic
# enabled : Enabled flag. When false, draw semi-transparently.
#--------------------------------------------------------------------------
  def draw_face(face_name, face_index, x, y, enabled = true)
    return super(face_name, face_index, x, y, enabled) unless MembersPlus::Five_Party_Trunc_Faces
      bitmap = Cache.face(face_name)
      rect = Rect.new(face_index % 4 * 96, face_index / 4 * 96, 96, 96)
      rect.width = [rect.width,item_width].min
      contents.blt(x, y, bitmap, rect, enabled ? 255 : translucent_alpha)
      bitmap.dispose
    end
  end
