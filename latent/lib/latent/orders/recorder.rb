class Recorder

  def record_order order
    colours = {}

    # i have no idea if this works or not because i haven't
    # been able to test it yet
    order.each do | o |
      if colours.has_key? o.colour
        # syntax?
        colours[o.colour]= colours[o.colour] + o.volume
      else
        colours[o.colour]= o.volume
      end
    end

    record_summarised_orders colours
  end

  def record_summarised_orders colours
    #raise 'this should be doing some io'
  end

end
